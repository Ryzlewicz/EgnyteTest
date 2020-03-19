Feature: Login
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background:
	Given Setup driver

@Login
Scenario Outline: Check system behavior when valid password is entered
	Given I open '<Browser>' browser and login
	Then Page URL is correct

	@Chrome
	Examples:
		| TestID | Browser |
		| 1      | Chrome  |
	@InternetExplorer
	Examples:
		| TestID | Browser |
		| 1      | IE      |
	@Firefox
	Examples:
		| TestID | Browser |
		| 1      | Firefox |

Scenario Outline: Check system behavior when invalid password is entered
	Given I open '<Browser>' browser and I enter <Password>
	Then Error <Message> message is displayed

	Examples:
		| Password      | Message                        |
		| wRoNgPaSsWoRd | Incorrect password. Try again. |
		|               | First enter password           |

	@Chrome
	Examples:
		| TestID | Browser |
		| 2      | Chrome  |
	@InternetExplorer
	Examples:
		| TestID | Browser |
		| 2      | IE      |
	@Firefox
	Examples:
		| TestID | Browser |
		| 2      | Firefox |

@Download
Scenario Outline: Use Download Folder button in root folder
	Given I open '<Browser>' browser and login
	When I click Download 'Folder' button
	Then Downloaded file is correct

	@Chrome
	Examples:
		| TestID | Browser |
		| 3      | Chrome  |
	@InternetExplorer
	Examples:
		| TestID | Browser |
		| 3      | IE      |
	Examples:
		@Firefox
		| TestID | Browser |
		| 3      | Firefox |

@Download
Scenario Outline: Use Download Selected button with one folder selected
	Given I open '<Browser>' browser and login
	When I check '1' checkbox
	And I click Download 'Selected' button
	Then Downloaded file2 is correct

	@Chrome
	Examples:
		| TestID | Browser |
		| 4      | Chrome  |
	@InternetExplorer
	Examples:
		| TestID | Browser |
		| 4      | IE      |
	@Firefox
	Examples:
		| TestID | Browser |
		| 4      | Firefox |

Scenario Outline: Use Download Selected button with one subfolder selected"
	Given I open '<Browser>' browser and login
	When Go to folder '1'
	And I check '0' checkbox
	And I click Download 'Selected' button
	Then Downloaded file2 is correct

	@Chrome
	Examples:
		| TestID | Browser |
		| 5      | Chrome  |
	@InternetExplorer
	Examples:
		| TestID | Browser |
		| 5      | IE      |
	@Firefox
	Examples:
		| TestID | Browser |
		| 5      | Firefox |

Scenario Outline: Use Download Selected button with selected folders and files
	Given I open '<Browser>' browser and login
	When I check '0' checkbox
	Then Downloaded file is correct

	@Chrome
	Examples:
		| TestID | Browser |
		| 6      | Chrome  |
	@InternetExplorer
	Examples:
		| TestID | Browser |
		| 6      | IE      |
	@Firefox
	Examples:
		| TestID | Browser |
		| 6      | Firefox |