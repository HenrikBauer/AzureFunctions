# Azure Functions Lab 

* [Installation](lab1-installation.md): Hinweise zur notwendigen Installation
* [Lab Part 1: Code bereitstellen](lab1-part1.md): Im ersten Teil wird der Lab-Code bereitgestellt, die Testanwendung zum Laufen gebracht und der Code analysiert.
* [Lab Part 2: Azure Functions einführen](lab1-part2.md): In diesem Teil wird eine Azure Functions App angelegt und die Berechnungslogik für die Bilder dorthin verlagert. 
* [Lab Part 3: Azure Functions optimieren](lab1-part3.md): In diesem Teil wird die Verarbeitung durch weitere Functions entkoppelt und parallelisiert.
* [Lab Part 4: Azure Functions in Azure deployen](lab1-part4.md): Im letzten Teil wird die Function App nach Azure deployt.


## Lab Part 1: Code bereitstellen
Im ersten Teil wird der Lab-Code bereitgestellt, die Testanwendung zum Laufen gebracht und der Code analysiert.   



### Schritt 1: GIT Clone
Bereitstellen der Sourcen:

	cd \
	md sdxlab
	cd sdxlab
	git clone https://github.com/SDXag/AzureFunctions


### Schritt 2: Testanwendung zum Laufen bringen 

Testanwendung bauen:

	cd AzureFunctions\lab1\src.web\SDX.FunctionsDemo.Web
	dotnet build
	dotnet run

Testanwendung verwenden, z.B. in Chrome:

	"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" https://localhost:5001


1. Bild auswählen (nur .png!), Bilder liegen unter *\sdxlab\AzureFunctions\Bilder*.
2. Bild hochladen
	* Das Bild wird auf den Server übertragen und dort verarbeitet; erst wenn die Verarbeitung fertig ist, wird auf die Ergebnisseite gewechselt.
	* Die Verarbeitung besteht aus dem Erstellen von weiteren Bildern in unterschiedlicher Auflösung bzw. mit Effekten.
	* *Hinweis: Um das Problem deutlich zu machen wurde die Verarbeitung künstlich verlängert!*
3. Auf der Ergebnisseite werden die berechneten Bilder angezeigt.

Quellcode analysieren:

	cd \sdxlab\AzureFunctions\lab1\src.web\SDX.FunctionsDemo.Web
	code .

* SDX.FunctionsDemo.Web: Enthält die ASP.NET Core 2.2 Anwendung
	* Der HomeController nutzt den *IImageFileService* um das Bild an die Logik zu übergeben und die berechneten Bilder abzurufen.
	* In *Startup.cs* wird *InMemoryImageFileService* angemeldet, der das im Speicher tut. 
* SDX.FunctionsDemo.ImageProcessing: Enthält den Code für die Bildverarbeitung


