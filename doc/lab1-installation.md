# Azure Functions Lab 

##Inhaltsverzeichnis
* [Lab-Übersicht](lab1.md)
	* [Installation](lab1-installation.md)
	* [Lab Part 1: Code bereitstellen](lab1-part1.md)
	* [Lab Part 2: Azure Functions einführen](lab1-part2.md)
	* [Lab Part 3: Azure Functions optimieren](lab1-part3.md)
	* [Lab Part 4: Azure Functions in Azure deployen](lab1-part4.md)



## Installation


### Hinweise:

* Das Lab verwendet Visual Studio Code (VSCode) um auf Code-Ebene plattformunabhägig zu sein.
	* Die Nutzung von Visual Studio 2017/2019 ist ebenfalls möglich und sollte wenig Probleme bereiten. Notwendige Informationen zur Installation finden sich hier: https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-your-first-function-visual-studio
* Das Lab wurde unter Windows entwickelt und gestestet; für Unterschiede bei Installation oder Kommandozeilensyntax wird auf die einschlägige Dokumentation verwiesen. 




###Installation (unter Windows):

* Visual Studio Code:	
	* Installer: https://code.visualstudio.com/
	* C# for Visual Studio Code:	
		* https://marketplace.visualstudio.com/itemdetails?itemName=ms-vscode.csharp
	* Azure Functions for Visual Studio Code: 
		* https://marketplace.visualstudio.com/itemdetails?itemName=ms-azuretools.vscode-azurefunctions 
		* vscode:extension/ms-azuretools.vscode-azurefunctions
* .NET Core 2.2 SDK:	
	* Installer: https://dotnet.microsoft.com/download
* Node.js:				
	* https://docs.npmjs.com/downloading-and-installing-node-js-and-npm 
	* Installer: https://nodejs.org/en/download/
*  Azure Functions Core Tools:	
	*  ``npm install -g azure-functions-core-tools``
	*  Weitere Optionen/Betriebssysteme: https://github.com/Azure/azure-functions-core-tools/blob/master/README.md#windows
* GIT: 
	* Installer: https://git-scm.com/downloads



###Azure Storage Emulator:

Für Windows steht der Azure Storage Emulator als Teil des Azure SDK zu Verfügung: https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator#get-the-storage-emulator

Unter anderen Betriebsystemen muss in Azure ein Storage Account angelegt werden: https://azure.microsoft.com/de-de/services/storage/



###Optional:

* Azure Storage Explorer:	
	* https://azure.microsoft.com/en-us/features/storage-explorer/
* Postman, curl, oder vergleichbar.



###Bei Problemen bzw. Installation unter Linux/MacOS:

Die oben angegebenen Links bieten auch Hinweise zur Installation unter Linux und MacOS.

Bei Problemen sollte die Dokumentation weiterhelfen können: https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code#prerequisites


