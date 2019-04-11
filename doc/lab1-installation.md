# Azure Functions Lab 

* [Installation](lab1-installation.md): Hinweise zur notwendigen Installation
* [Lab Part 1: Code bereitstellen](lab1-part1.md): Im ersten Teil wird der Lab-Code bereitgestellt, die Testanwendung zum Laufen gebracht und der Code analysiert.
* [Lab Part 2: Azure Functions einführen](lab1-part2.md): In diesem Teil wird eine Azure Functions App angelegt und die Berechnungslogik für die Bilder dorthin verlagert. 
* [Lab Part 3: Azure Functions optimieren](lab1-part3.md): In diesem Teil wird die Verarbeitung durch weitere Functions entkoppelt und parallelisiert.
* [Lab Part 4: Azure Functions in Azure deployen](lab1-part4.md): Im letzten Teil wird die Function App nach Azure deployt.


## Installation

Hinweis: Das Lab verwendet Visual Studio Code. Weiter unten gibt es Hinweise für Nutzer von Visual Studio 2017/2019.  

Installation unter Windows:

* Visual Studio Code:	Installer: https://code.visualstudio.com/
	* C# for Visual Studio Code:	https://marketplace.visualstudio.com/itemdetails?itemName=ms-vscode.csharp
	* Azure Functions for Visual Studio Code: https://marketplace.visualstudio.com/itemdetails?itemName=ms-azuretools.vscode-azurefunctions / vscode:extension/ms-azuretools.vscode-azurefunctions
* .NET Core 2.2 SDK:	Installer: https://dotnet.microsoft.com/download
* Node.js:				https://docs.npmjs.com/downloading-and-installing-node-js-and-npm / Installer: https://nodejs.org/en/download/
*  Azure Functions Core Tools:	
	*  ``npm install -g azure-functions-core-tools``
	*  Weitere Optionen: https://github.com/Azure/azure-functions-core-tools/blob/master/README.md#windows
* GIT: Installer: https://git-scm.com/downloads


Azure Storage Emulator:
Für Windows steht der Azure Storage Emulator als Teil des Azure SDK zu Verfügung: https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator#get-the-storage-emulator
Unter anderen Betriebsystemen muss in Azure ein Storage Account angelegt werden: https://azure.microsoft.com/de-de/services/storage/

Optional:

* Azure Storage Explorer:	https://azure.microsoft.com/en-us/features/storage-explorer/
* Postman, curl, oder vergleichbar.


Bei Problemen bzw. Installation unter Linux/MacOS:

Die oben angegebenen Links bieten auch Hinweise zur Installation unter Linux und MacOS.
Bei Problemen sollte die Dokumentation weiterhelfen können: https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code#prerequisites
Er Visual Studio verwenden will, findet hier die notwendigen Informationen: https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-your-first-function-visual-studio

