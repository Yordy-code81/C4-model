﻿using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            Banking();
        }

        static void Banking()
        {
            const long workspaceId = 82329;
            const string apiKey = "a0159479-dfe0-4db5-88e6-6e4ad0d7aeef";
            const string apiSecret = "5765803b-dfa9-4620-89d7-3cb170eec12f";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);
            Workspace workspace = new Workspace("C4 Model - Easy Store", "Sistema sample-content");
            ViewSet viewSet = workspace.Views;
            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem librarySystem = model.AddSoftwareSystem("Easy Store", "Plataforma web donde puedes publicar y leer historias de otros usuarios regitrados pudiendo suscribirse a contenido exclusivo");
                        
            Person escritor = model.AddPerson("Escritor", "Usuario capaz de publicar contenido textual.");
            Person lector = model.AddPerson("Lector", "Usuario que solo podra leer contenido y suscribirse");

            
            lector.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de las publicaciones que puede leer");
            escritor.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de los lectors que seleccionan su negocio");

            
            SystemContextView contextView = viewSet.CreateSystemContextView(librarySystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A3_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags
            escritor.AddTags("Ciudadano");
            lector.AddTags("Ciudadano");
            librarySystem.AddTags("SistemaLibros");


            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Ciudadano") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("SistemaLibros") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });

            

            // 2. Diagrama de Contenedores
            Container mobileApplication =       librarySystem.AddContainer("Mobile App", "Permite a los usuarios visualizar las actividades turisticas que pueden realizar cercanas a su destino.", "Flutter");
            Container webApplication =          librarySystem.AddContainer("Web App", "Permite a los usuarios visualizar las actividades turisticas que pueden realizar cercanas a su destino.", "Vue");
            Container landingPage =             librarySystem.AddContainer("Landing Page", "", "Bootstrap");
            Container pagoContext =             librarySystem.AddContainer("Pagos Context", "Bounded Context del Microservicio de pagos de suscripciones", "NodeJS (NestJS)");
            Container historiasContext =        librarySystem.AddContainer("Post Context", "Bounded Context del Microservicio de contenido de las historias", "NodeJS (NestJS)");
            Container userContext =             librarySystem.AddContainer("User Context", "Bounded Context del Microservicio para users", "NodeJS (NestJS)");
            Container apiGateway =              librarySystem.AddContainer("API Gateway", "API Gateway", "Spring Boot port 8080");
            Container database1 =               librarySystem.AddContainer("Pagos DB", "", "MySQL");
            Container database3 =               librarySystem.AddContainer("Post DB", "", "MySQL");
            Container database5 =               librarySystem.AddContainer("Post DB Replica", "", "MySQL");
            Container database6 =               librarySystem.AddContainer("User DB", "", "MySQL");
            Container messageBus = librarySystem.AddContainer("Bus de Mensajes en Cluster de Alta Disponibilidad", "Transporte de eventos del dominio.", "RabbitMQ");

            
            

            lector.Uses(mobileApplication, "Consulta");
            lector.Uses(webApplication, "Consulta");
            lector.Uses(landingPage, "Consulta");
            escritor.Uses(mobileApplication, "Consulta");
            escritor.Uses(webApplication, "Consulta");
            escritor.Uses(landingPage, "Consulta");
                     

            mobileApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            webApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");

            apiGateway.Uses(pagoContext,         "Request", "JSON/HTTPS");
            apiGateway.Uses(historiasContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(userContext,   "Request", "JSON/HTTPS");

            pagoContext.Uses(database1, "", "JDBC");
            historiasContext.Uses(database3, "", "JDBC");
            historiasContext.Uses(database5, "", "JDBC");
            database3.Uses(database5, "", "JDBC");
            userContext.Uses(database6, "", "JDBC");
            
            pagoContext.Uses(messageBus,"Publica y consume eventos del dominio");
            historiasContext.Uses(messageBus, "Publica y consume eventos del dominio");
            userContext.Uses(messageBus, "Publica y consume eventos del dominio");
                        
            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");            
            apiGateway.AddTags("APIGateway");
            database1.AddTags("Database");
            database3.AddTags("Database");
            database5.AddTags("Database");
            database6.AddTags("Database");
            pagoContext.AddTags("BoundedContext");            
            historiasContext.AddTags("BoundedContext");            
            userContext.AddTags("BoundedContext");
            messageBus.AddTags("MessageBus");


            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIGateway") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("BoundedContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MessageBus") { Width = 850, Background = "#fd8208", Color = "#ffffff", Shape = Shape.Pipe, Icon = "" });


            ContainerView containerView = viewSet.CreateContainerView(librarySystem, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements(); 
            
            //3. Diagrama de Componentes -> post
            Component domainLayerHistoriasContext = historiasContext.AddComponent("Domain Layer", "Domino del contexto", "Spring Boot(NestJS)");
            Component historiaController = historiasContext.AddComponent("History Controller", "REST API endpoint de historias", "Spring Boot");
            Component historiaApplicationService = historiasContext.AddComponent("HistorysAplication Service", "Prove metodos para los datos de hisotrias", "Spring Boot");
            Component historiaRepository=historiasContext.AddComponent("History Repository", "Información de historias", "Spring Boot");
            Component usuarioRepository=historiasContext.AddComponent("Usuario Repository", "Información de usuarios", "Spring Boot");

            mobileApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            webApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            apiGateway.Uses(historiaController, "JSON");
            historiaController.Uses(historiaApplicationService, "");
            historiaApplicationService.Uses(historiaRepository, "");
            historiaApplicationService.Uses(usuarioRepository, "");
            historiaApplicationService.Uses(domainLayerHistoriasContext, "");
            historiaRepository.Uses(database5, "", "JDBC");
            historiaRepository.Uses(database3, "", "JDBC");
            database3.Uses(database5, "", "JDBC");            

            domainLayerHistoriasContext.AddTags("Component");
            historiaController.AddTags("Component");
            historiaApplicationService.AddTags("Component");
            historiaRepository.AddTags("Component");
            usuarioRepository.AddTags("Component");
           
            styles.Add(new ElementStyle("Component") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
         

            ComponentView historyComponentView = viewSet.CreateComponentView(historiasContext, "History Components", "Component Diagram");
            historyComponentView.PaperSize = PaperSize.A4_Landscape;
            historyComponentView.Add(mobileApplication);   
            historyComponentView.Add(apiGateway);   
            historyComponentView.Add(webApplication);
            historyComponentView.Add(database5);
            historyComponentView.Add(database3);
            historyComponentView.AddAllComponents();

            //3. Diagrama de Componentes -> user
            Component userController = userContext.AddComponent("User Controller", "REST API endpoint de user", "Spring Boot");
            Component userApplicationService = userContext.AddComponent("Users Aplication Service", "Prove metodos para los datos de user", "Spring Boot");
            Component userRepository=userContext.AddComponent("User Repository", "Información de user", "Spring Boot");
            Component userDomainLayer=userContext.AddComponent("Domain Layer", "Dominio del contexto", "Spring Boot");

            mobileApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            webApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            apiGateway.Uses(userController, "JSON");
            userController.Uses(userApplicationService, "");
            userApplicationService.Uses(userRepository, "");
            userApplicationService.Uses(userDomainLayer, "");
            userRepository.Uses(database6, "", "JDBC");

            userController.AddTags("Component");
            userApplicationService.AddTags("Component");
            userRepository.AddTags("Component");
            userDomainLayer.AddTags("Component");
           

            ComponentView userComponentView = viewSet.CreateComponentView(userContext, "User Components", "Component Diagram");
            userComponentView.PaperSize = PaperSize.A4_Landscape;
            userComponentView.Add(mobileApplication);   
            userComponentView.Add(apiGateway);   
            userComponentView.Add(webApplication);
            userComponentView.Add(database6);
            userComponentView.AddAllComponents();    

            //3. Diagrama de Componentes -> pago
            Component pagoController = pagoContext.AddComponent("Pago Controller", "REST API endpoint de Pago", "Spring Boot");
            Component pagoApplicationService = pagoContext.AddComponent("Pago Aplication Service", "Prove metodos para los datos de Pagos", "Spring Boot");
            Component pagoRepository=pagoContext.AddComponent("Pago Repository", "Información de Pago", "Spring Boot");
            Component pagoDomainLayer=pagoContext.AddComponent("Pago Domain Layer", "Dominio del contexto", "Spring Boot");

            mobileApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            webApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            apiGateway.Uses(pagoController, "JSON");
            pagoController.Uses(pagoApplicationService, "");
            pagoApplicationService.Uses(pagoRepository, "");
            pagoApplicationService.Uses(pagoDomainLayer, "");
            pagoRepository.Uses(database1, "", "JDBC");

            pagoController.AddTags("Component");
            pagoApplicationService.AddTags("Component");
            pagoRepository.AddTags("Component");
            pagoDomainLayer.AddTags("Component");
           

            ComponentView pagoComponentView = viewSet.CreateComponentView(pagoContext, "Pago Components", "Component Diagram");
            pagoComponentView.PaperSize = PaperSize.A4_Landscape;
            pagoComponentView.Add(mobileApplication);   
            pagoComponentView.Add(apiGateway);   
            pagoComponentView.Add(webApplication);
            pagoComponentView.Add(database1);
            pagoComponentView.AddAllComponents();          

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}