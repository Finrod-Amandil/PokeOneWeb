workspace {
    model {
        user = person "Website Visitor" {
            description "A player of PokéOne"
        }
        editor = person "Guide Editor" {
            description "A member of the editorial team of the PokéOne Guide"
        }
        softwareSystem = softwareSystem "PokeOneWeb" {
            description "Allows players of PokéOne to look up and search through PokéOne-related data."
            frontend = container "Web Application Frontend" {
                tags "Browser Application"
                technology "Angular"
                description "Single page application which presents the PokéOne guide data along with helpful filters and tools."
                
                apiServices = component "API Services" {
                    technology "Typescript class"
                    description "Execute HTTPS requests to load application data from the backend"
                }
                
                router = component "Custom Router" {
                    technology "Angular Route Guard"
                    description "Loads correct page depending on non-hierarchical route by looking up the entity type of the requested path/resource name"
                }
                
                pages = component "Content Pages" {
                    technology "Angular Module"
                    description "Displays application data with filters, tools and similar"
                }
                
                apiServices -> router "Provides data for dynamic routing"
                apiServices -> pages "Provides data"
            }
            updateService = container "Update Service" {
                technology "C#/.NET"
                description "Automatically imports the changes made in the Google Spreadsheets and updates the application database, JSON files and spreadsheet guide."
                
                spreadsheetImport = component "Google Spreadsheet Import Service" {
                    technology "C#"
                    description "Checks whether any of the import spreadsheets have changes, then imports and validates these changes and stores them in the application database."
                }
                
                readModelUpdate = component "ReadModel Update Service" {
                    technology "C#"
                    description "Generates the preprocessed, ready-to-serve JSON files using the data in the application database."
                }
                
                spreadsheetGuideUpdate = component "Spreadsheet Guide Update Service" {
                    technology "C#"
                    description "Updates the Google Spreadsheet of the existing PokéOne Guide so that it remains up to date with the web application."
                }
            }
            sheets = container "Import sheets" {
                tags "Browser Application"
                tags "External System"
                technology "Google Spreadsheets"
                description "Tool for editing the raw PokéOne Guide data"
            }
            applicationDb = container "Database" {
                tags "Database"
                technology "Relational Database Schema"
                description "Stores a cleaned and redundancy-free model of the PokéOne Guide data."
            }
            json = container "JSON Files" {
                tags "Files"
                technology "JSON"
                description "PokéOne guide data in reprocessed form, ready to be served to the Frontend."
            }
            spreadsheetGuide = container "PokéOne Guide (Spreadsheet version)" {
                tags "Browser Application"
                tags "External System"
                technology "Google Spreadsheets"
                description "The 'old' PokéOne Guide, presented as a Google Spreadsheet"
            }
            
            user -> frontend "Looks up specific information about PokéOne"
            apiServices -> json "Gets data from" "HTTPS/JSON"
            editor -> sheets "Maintains existing data and adds new data"
            sheets -> spreadsheetImport "Gets imported by" "Google Sheets API"
            spreadsheetImport -> applicationDb "Cleans and updates changed data" "Entity Framework"
            applicationDb -> readModelUpdate "Provides clean, structured application data" "Entity Framework"
            readModelUpdate -> json "Exports database data as" "File System"
            applicationDb -> readModelUpdate "Provides clean, structured application data" "Entity Framework"
            spreadsheetGuideUpdate -> spreadsheetGuide "Exports database data as" "Google Sheets API"
            applicationDb -> spreadsheetGuideUpdate "Provides clean, structured application data"
        }
    }

    views {
        systemContext softwareSystem {
            include *
            autolayout lr
        }

        container softwareSystem {
            include *
            autolayout lr
        }
        
        component frontend {
            include *
            autolayout lr
        }
        
        component updateService {
            include *
            autolayout lr
        }

        theme default
        
        styles {
            element "Browser Application" {
                shape WebBrowser
            }
            element "Database" {
                shape Cylinder
            }
            element "Files" {
                shape Folder
            }
            element "External System" {
                background #AAAAAA
                stroke #666666
            }
        }
    }
}