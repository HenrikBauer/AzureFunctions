# Azure Functions Lab 

![af.png](images/af.png)

Dieses Lab soll den ersten Einstieg in Azure Functions bieten.



### Motivation
Die Dokumentation von Microsoft bietet einen Einstige in Azure Functions auf rein technischer Ebene. In diesem Lab wurde Wert darauf gelegt, dass der Einsatz im Rahmen eines wenn auch vereinfachten, aber doch nachvollziehbaren Use Cases stattfindet.

In einer Web-Anwendung können Bilder hochgeladen werden, die dann auf verschiedene Arten aufbereitet werden (unterschiedliche Größen, Filtereffekte, runder Ausschnitt):

[![app2.png](images/app2.png)](images/app1.png)

Die Verarbeitung der Bilder dauert entsprechend lange und soll deshalb in eine Azure Function verlagert werden.

>Hinweis: Um den Effekt deutlich zu machen, wurde die Berechnung künstlich verlängert.


## Voraussetzungen

Kenntnisse von C# werden vorausgesetzt. Kenntnisse von ASP.NET MVC sind von Vorteil. 

Es wird ein Azure-Konto benötigt. Fall kein Konto verfügbar ist, kann ein kostenloses Azure-Konto angelegt werden: https://azure.microsoft.com/de-de/free/

Notwendigkeit eines Azure-Kontos:

* Unter Windows: Der Account wird nur für den letzten Teil (Azure Deployment) benötigt, die Entwicklung ist lokal möglich.
* Unter anderen Betriebssystemen: Der Account wird auch für die Entwicklung benöigt, da es keinen Storage Emulator gibt.
	>Mögliche Alternative (nicht getestet): https://github.com/azure/azurite



## Lab-Inhalte

* [Installation](lab1-installation.md): Hinweise zur notwendigen Installation
* [Lab Part 1: Code bereitstellen](lab1-part1.md): Im ersten Teil wird der Lab-Code bereitgestellt, die Testanwendung zum Laufen gebracht und der Code analysiert.
* [Lab Part 2: Azure Functions einführen](lab1-part2.md): In diesem Teil wird eine Azure Functions App angelegt und die Berechnungslogik für die Bilder dorthin verlagert. 
* [Lab Part 3: Azure Functions optimieren](lab1-part3.md): In diesem Teil wird die Verarbeitung durch weitere Functions entkoppelt und parallelisiert.
* [Lab Part 4: Azure Functions in Azure deployen](lab1-part4.md): Im letzten Teil wird die Function App nach Azure deployt.



# Links
* SDX AG
	* https://www.sdx-ag.de
	* https://www.sdx-ag.de/?s=azure+functions
	* https://www.sdx-ag.de/2019/03/azure-functions-eine-kurze-einfhrung/##news_content
* Microsoft
	* https://azure.microsoft.com/de-de/services/functions/
	* https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code


