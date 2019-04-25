# Azure Functions Lab 

## Inhaltsverzeichnis
* [Lab-Übersicht](lab1.md)
	* [Installation](lab1-installation.md)
	* [Lab Part 1: Code bereitstellen](lab1-part1.md)
	* [Lab Part 2: Azure Functions einführen](lab1-part2.md)
	* [Lab Part 3: Azure Functions optimieren](lab1-part3.md)
	* [Lab Part 4: Azure Functions in Azure deployen](lab1-part4.md)
	* [Lab Part 5: Azure Functions absichern](lab1-part5.md)



## Lab Part 3: Azure Functions optimieren

In diesem Teil wird die Verarbeitung durch weitere Functions entkoppelt und parallelisiert:
* Die Bilder werden nicht mehr beim Upload berechnet; stattdessen wird ein Eintrag in einer Message Queue je Bildberechnung angelegt.
* Eine neue Queue-Function arbeitet die Queue-Einträge ab.



### Schritt 1: Anpassen der Function *UploadImage*

Wir werden Informationen als JSON in eine Queue stellen. Dazu wird eine neue Datenklasse angelegt:

```CSharp
namespace SDX.FunctionsDemo.FunctionApp
{
    public class ProcessImageMessage
    {
        public string ID { get; set; }
        public string ImageType { get; set; }
        public string FileName { get; set; }
    }
}
```


In VSCode wird die Function *UploadImage* angepaßt.
Wir benötigen Zugriff auf die Queue, daher muss die Zeile für den Zugriff auf den Blob Container geändert werden:

```CSharp
// ALT: var container = context.GetConfiguration().GetStorageAccount().GetBlobContainer(StorageDefines.Blobs.Images);
// NEU:
var account = context.GetConfiguration().GetStorageAccount();
var container = account.GetBlobContainer(StorageDefines.Blobs.Images);
var queue = account.GetQueue(StorageDefines.Queues.ProcessImage);
```

In der Schleife für die Verarbeitung muss die Berechnung der Bilder durch das Einstellen einer Message in eine Queue ersetzt werden: 

```CSharp
// ALT: var image = ImageUtils.ProcessImage(data, imageType, fileName);
// ALT: blobName = BlobNameHelper.CreateBlobName(id, imageType);
// ALT: await container.UploadBlobAsync(blobName, image);
// ALT: log.LogInformation("Image processed: " + blobName);
// NEU:
var message = new ProcessImageMessage
{
    ID = id,
    ImageType = imageType,
    FileName = fileName
};
var json = JsonConvert.SerializeObject(message);
await queue.AddMessageAsync(json);
log.LogInformation("Image processing requested: " + json);
```


Zum Test der Anwendung muss die Function App gestoppt, neu gebaut und gestartet werden:

	cd /sdxlab/AzureFunctions/lab1/src.func/SDX.FunctionsDemo.FunctionApp
	dotnet build
	func host start

Testet man die Anwendung erneut im Browser, so findet der Wechsel von der Upload-Seite auf die Ergebnisseite deutlich schneller statt. Allerdings stehen die erwarteten Bilder natürlich nicht zur Verfügung, stattdessen werden Platzhalter angezeigt.  



### Schritt 2: Queue-Function zur Berechnung der Bilder

Zur Berechnung der Bilder wird eine neue Function benötigt:

* in VSCode: "Create Function"
	* Sprache: 			`C#`
	* Function Type: 	`QueueTrigger`
	* Name: 			`ProcessImage`
	* Namespace:		`SDX.FunctionsDemo.FunctionApp`
	* Settings key:		`AzureWebJobsStorage`
	* Queue	name:  		`processimage` (lowercase!)
	
Es ist wichtig, dass der Name der Queue in Kleinbuchstaben angegeben wird; Storage hat hier leider einige Einschränkungen: https://docs.microsoft.com/en-us/rest/api/storageservices/naming-queues-and-metadata

Folgende `using`-Direktiven werden benötigt:

```CSharp
using System.IO;
using SDX.FunctionsDemo.FunctionApp.Utils;
using SDX.FunctionsDemo.ImageProcessing;
```


Die Function-Methode ist anzupassen. Der gesamte Funktionsrumpf kann gelöscht werden. Der Parameter mit dem Attribut `QueueTrigger` ist als `string` deklariert. Da es sich in der Message um JSON handelt, können wir direkt den neuen Typ `ProcessImageMessage` angeben. Außerdem muss der `ExecutionContext` wieder ergänzt werden:

```CSharp
[FunctionName("ProcessImage")]
public static async Task RunAsync(
    [QueueTrigger("processimage", Connection = "AzureWebJobsStorage")]ProcessImageMessage message,  // <<-- angepaßt !!!
    ExecutionContext context,		// <<-- NEU !!!
    ILogger log)
{
```

Zunächst müssen wir das Orginalbild laden, das von der HTTP-Function im Blob Stoage abgelegt wurde.  

```CSharp
var id = message.ID;
var imageType = message.ImageType;
var fileName = message.FileName;

var container = context
    .GetConfiguration()
    .GetStorageAccount()
    .GetBlobContainer(StorageDefines.Blobs.Images);

var blobName = BlobNameHelper.CreateBlobName(id, ImageUtils.ImageTypeOriginal);
var blob = container.GetBlobReference(blobName);

var strm = await blob.OpenReadAsync();
var ms = new MemoryStream();
strm.CopyTo(ms);
var data = ms.ToArray();
```

Die Berechnung und das Speichern entspricht dem ursprünglichen Schleifenkörper: 

```CSharp
var image = ImageUtils.ProcessImage(data, imageType, fileName);
blobName = BlobNameHelper.CreateBlobName(id, imageType);
await container.UploadBlobAsync(blobName, image);
log.LogInformation("Image processed: " + blobName);
```


Wieder muss die Function App gestoppt, neu gebaut und gestartet werden:

	cd /sdxlab/AzureFunctions/lab1/src.func/SDX.FunctionsDemo.FunctionApp
	dotnet build
	func host start

Ein erneuter Test der die Anwendung zeigt:

* Nach einem Upload findet der Wechsel auf die Ergebnisseite deutlich schneller statt. 
* Da die Bilder noch nicht zur Verfügung stehen, werden Platzhalter angezeigt.
* Die Queue-Functions werden nun im Hintergrund parallel abgearbeitet.
* Da die Anwendung ein Auto-Refresh macht, werden die Bilder mit einiger Verzögerung angezeigt.

Hinweis: Beim allerersten Mal dauert es einen Moment, bis die Queue-Verarbeitung startet.

