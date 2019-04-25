# Azure Functions Lab 

## Inhaltsverzeichnis
* [Lab-Übersicht](lab1.md)
	* [Installation](lab1-installation.md)
	* [Lab Part 1: Code bereitstellen](lab1-part1.md)
	* [Lab Part 2: Azure Functions einführen](lab1-part2.md)
	* [Lab Part 3: Azure Functions optimieren](lab1-part3.md)
	* [Lab Part 4: Azure Functions in Azure deployen](lab1-part4.md)



## Lab Part 4: Azure Functions in Azure deployen

In diesem Teil wird die Function App nach Azure deployt. Die Function App sollte dabei im Azure Portal angelegt werden. 

>Zwar bietet auch VSCode an, die Function App zu erstellen, dabei können jedoch bestimmte Dinge nicht vorgegeben werden, z.B. die Region.



### Schritt 1: Anlegen der Function App im Azure Portal

Hierzu sind folgende Schritte im Azure Portal notwendig: https://portal.azure.com

* Anmelden und ggf. die korrekte Subscription auswählen

* Eine neue *Resource group* anlegen
	* Beispiel für den Namen: `azurefunctionslab1`
	* Region: `West Europe` 

	>Eine *Resource group* kann nach Beendingung des Labs mit allen enthalten Resourcen in einem Rutsch wieder gelöscht werden. 

* Anlegen einer neuen Function App:
	* "Create resource", "Function App" suchen und auswählen:

		![azure-fa.png](images/azure-fa.png)

	* Die Function sauber parametrisieren:  
		* Der `App name` muss global eindeutig sein, das hier verwendete `azurefunctionslab1` kann also *nicht* genutzt werden.
		* Bei `Resource Group` muss die gerade angelegte *Resource Group* ausgewählt werden.
		* `Hosting Plan` = `Consumption Plan` stellt sicher, dass nach Verbrauch abgerechnet wird.
		* Location: `West Europe` 
		* `Application Insights` kann abgewählt werden, gibt aber durchaus Einblicke in die Nutzung der Function 
		
		![azure-fa2.png](images/azure-fa2.png)
	 
>Hinweis: Das Anlegen der Function App kann gelegentlich mehr Zeit in Anspruch nehmen, als auf den ersten Blick erkennbar. 

Danach sollten mindestens die Function App (am Icon erkennbar, als *App Service* ausgewiesen) und ein Storage Account verfügbar sein:

![azure-fa3.png](images/azure-fa3.png)

Nach Auswahl der Function App kann unter "Overview" die URL ermittelt werden, in diesem Fall https://azurefunctionslab1.azurewebsites.net 



### Schritt 2: Anpassen der Web-Anwendung

In der Web-Anwendung muss die URL der Function App von der lokalen Umgebung auf die neue Function App in Azure geändert werden.

	cd /sdxlab/AzureFunctions/lab1/src.web
	code .

Im Projekt `SDX.FunctionsDemo.Web` die Datei `appsettings.json` öffnen und die URL ändern:

```JavaScript
"Settings": {
    "FunctionApp.Url": "https://azurefunctionslab1.azurewebsites.net"
}
```

Hinweis: Zur Sicherheit sollte die lokal laufende Function App beendet werden. 


Danach die Web-Anwendung beenden, und neu starten:

	cd /sdxlab/AzureFunctions/lab1/src.web/SDX.FunctionsDemo.Web
	dotnet run
 
Ein neuer Test führt wieder zu Fehlermeldungen, weil die Function App in Azure noch nichts tut. Das wird sich im nächsten Schritt ändern.



### Schritt 3: Deployment der Function App

Das Function App Projekt in VSCode öffnen:

	cd /sdxlab/AzureFunctions/lab1/src.func
	code .

Das Deployment kann folgendermaßen durchgeführt werden:

* Im Explorer den Eintrag `SDX.FunctionsDemo.FunctionApp` selektieren, Kontextmenü, Eintrag "Deploy to Function App..." auswählen:

	![vscode2.png](images/vscode2.png)
* Die gerade angelegte Function App `azurefunctionslab1` als Ziel auswählen  
* Das Deployment bestätigen
* Das Deployment benötigt einen Moment...

Alternative: Unter dem Azure-Button links werden die Subscriptions afgelistet, wenn man in Azure angemeldet ist. Darunter findet sich die Function App. In deren Kontextmenü läßt sich das Deployment  ebenfalls anstossen. 



### Schritt 4: Test der Anwendung

Um die Verarbeitung der Functions sichtbar zu machen, ist es sinnvoll, Zugriff auf die Logs zu erhalten. Dies ist im Azure Portal möglich, oder auch in VSCode:

![azure-fa4.png](images/azure-fa4.png)

Die Lauffähigkeit der Function App läßt sich mit CURL verifizieren:

	CURL https://azurefunctionslab1.azurewebsites.net/api/ping

Ein neuer Test im Browser zeigt die Funktionsfähigkeit. 

Hinweis: Beim allerersten Mal dauert es einen Moment, bis die Queue-Verarbeitung startet.

