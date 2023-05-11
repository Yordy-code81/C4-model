using Structurizr;
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
            Workspace workspace = new Workspace("C4 Model - Technogym", "System sample-content");
            ViewSet viewSet = workspace.Views;
            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem librarySystem = model.AddSoftwareSystem("Technogym", "Plataforma que proporciona a las personas equipos de gimnasia de la más alta calidad, junto con otros servicios como contenido y programas");
            
            SoftwareSystem googleSystem = model.AddSoftwareSystem("Google", "Plataforma que proporciona Technogym servicios cloud como pagos y otros");
                        
            Person deportista = model.AddPerson("Deportista", "Usuario capaz de adquirir servicios fitness y de bienestar.");
            Person gimnasio = model.AddPerson("Organizacion health y fitness", "Organización que adquirirá equipos de alta calidad");
            Person dev = model.AddPerson("Desarrolladores", "Acceso al mywellness cloud API o al Unity SDK");

            
            deportista.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de los servicios que se ofrecen");
            gimnasio.Uses(librarySystem, "Realiza consultas para mantenerse al tanto de los equipos que ofrecen");
            dev.Uses(librarySystem, "Realiza consultas para el desarrollo de los microservices");

            librarySystem.Uses(googleSystem, "usa la api de google para diferentes servicios");

            
            SystemContextView contextView = viewSet.CreateSystemContextView(librarySystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A3_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags
            deportista.AddTags("Ciudadano");
            gimnasio.AddTags("Ciudadano");
            dev.AddTags("Ciudadano");
            librarySystem.AddTags("SistemaLibros");
            googleSystem.AddTags("SistemaGoogle");


            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Ciudadano") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("SistemaLibros") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("SistemaGoogle") { Background = "#f03a30", Color = "#ffffff", Shape = Shape.RoundedBox });

            

            // 2. Diagrama de Contenedores
            Container mobileApplication =       librarySystem.AddContainer("Mobile App", "Permite a los operadores conectarse con sus clientes", "Kotlin y Swift");
            Container webApplication =          librarySystem.AddContainer("Web App", "Permite a los operadores conectarse con sus clientes", "Angular");
            //Container landingPage =             librarySystem.AddContainer("Landing Page", "", "Bootstrap");
            Container sharedContext =             librarySystem.AddContainer("Shared Context", "Bounded Context del Microservicio de Shared Kernel con elementos base o compartidos con otros bounded contexts.", "NodeJS (NestJS)");
            Container identityContext =        librarySystem.AddContainer("Indentity Context", "Bounded Context del Microservicio de User Registry, Single-Sign-On para toda la plataforma, Authentication and Authorization Management", "NodeJS (NestJS)");
            Container ecommerceContext =             librarySystem.AddContainer("Ecommerce Context", "Bounded Context del Microservicio Gestión de venta de productos online, en puntos de venta y venta de suscripciones, incluyendo integración con pasarela de pagos, control de deudas y vencimientos", "NodeJS (NestJS)");
            Container inventoryContext =             librarySystem.AddContainer("Inventory Context", "Bounded Context del Microservicio Catálogo de productos y servicios, manejo de inventarios, adquisiciones de productos y servicios", "NodeJS (NestJS)");
            Container accountsContext =             librarySystem.AddContainer("Account Context", "Bounded Context del Microservicio Información de accounts, perfiles y preferencias para usuarios individuales y perfiles para clientes business y fitness clubs", "NodeJS (NestJS)");
            Container trainingContext =             librarySystem.AddContainer("Training Context", "Bounded Context del Microservicio Diseño, ejecución y seguimiento de planes de entrenamiento considerando por ejemplo objetivos cardiovasculares, peso, máquinas de technogym a utilizar, pesas, actividades, repeticiones y otros relacionados con training, accesibles por el usuario vía Technogym App o Mywellness", "NodeJS (NestJS)");
            Container businessContext =             librarySystem.AddContainer("Business Context", "Bounded Context del Microservicio para Los incluidos en Premium services for smart facilities, donde varios se integran con otros bounded contexts", "NodeJS (NestJS)");
            Container contentContext =             librarySystem.AddContainer("Content Context", "Bounded Context del Microservicio para Manejo del contenido de videos de entrenamiento, capacitación, assets de imagen, soporte de streaming para integrar en los productos de la plataforma", "NodeJS (NestJS)");
            Container digitalContext =             librarySystem.AddContainer("Digital Context", "Bounded Context del Microservicio para Elementos de IA y ML que ofrecen asistencia en sesiones de entrenamiento vía consolas de máquinas o aplicaciones para usuarios en la plataforma.", "NodeJS (NestJS)");
            Container dataContext =             librarySystem.AddContainer("Data Context", "Bounded Context del Microservicio para Gestión unificada de registros de actividades en los diversos productos y servicios que sirven de base para los asistentes de IA y la labor de los técnicos de mantenimiento de equipos de entrenamiento", "NodeJS (NestJS)");
            Container apiGateway =              librarySystem.AddContainer("API Gateway", "API Gateway", "Spring Boot port 8080");
            Container database1 =               librarySystem.AddContainer("Shared DB", "", "MySQL");
            Container database3 =               librarySystem.AddContainer("Ecommerce DB", "", "MySQL");
            Container database5 =               librarySystem.AddContainer("Ecommerce DB Replica", "", "MySQL");
            Container database2 =               librarySystem.AddContainer("Inventory DB", "", "MySQL");
            Container database4 =               librarySystem.AddContainer("Account DB", "", "MySQL");
            Container database6 =               librarySystem.AddContainer("Training DB", "", "MySQL");
            Container database7 =               librarySystem.AddContainer("Business DB", "", "MySQL");
            Container database8 =               librarySystem.AddContainer("Content DB", "", "MySQL");
            Container database9 =               librarySystem.AddContainer("Digital DB", "", "MySQL");
            Container database10 =               librarySystem.AddContainer("Data DB", "", "MySQL");
            Container database11 =               librarySystem.AddContainer("Indentity DB", "", "MySQL");
            Container messageBus = librarySystem.AddContainer("Bus de Mensajes en Cluster de Alta Disponibilidad", "Transporte de eventos del dominio.", "RabbitMQ");

            
            

            deportista.Uses(mobileApplication, "Consulta");
            deportista.Uses(webApplication, "Consulta");
            //lector.Uses(landingPage, "Consulta");
            gimnasio.Uses(mobileApplication, "Consulta");
            gimnasio.Uses(webApplication, "Consulta");
            dev.Uses(webApplication, "Consulta");
            //escritor.Uses(landingPage, "Consulta");
                     

            mobileApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");
            webApplication.Uses(apiGateway,"API Request", "JSON/HTTPS");

            apiGateway.Uses(sharedContext,         "Request", "JSON/HTTPS");
            apiGateway.Uses(identityContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(ecommerceContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(inventoryContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(accountsContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(trainingContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(businessContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(contentContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(digitalContext,   "Request", "JSON/HTTPS");
            apiGateway.Uses(dataContext,   "Request", "JSON/HTTPS");

            sharedContext.Uses(database1, "", "JDBC");
            ecommerceContext.Uses(database3, "", "JDBC");
            ecommerceContext.Uses(database5, "", "JDBC");
            database3.Uses(database5, "", "JDBC");
            inventoryContext.Uses(database2, "", "JDBC");
            accountsContext.Uses(database4, "", "JDBC");
            trainingContext.Uses(database6, "", "JDBC");
            businessContext.Uses(database7, "", "JDBC");
            contentContext.Uses(database8, "", "JDBC");
            digitalContext.Uses(database9, "", "JDBC");
            dataContext.Uses(database10, "", "JDBC");
            identityContext.Uses(database11, "", "JDBC");
            
            sharedContext.Uses(messageBus,"Publica y consume eventos del dominio");
            ecommerceContext.Uses(messageBus, "Publica y consume eventos del dominio");
            inventoryContext.Uses(messageBus, "Publica y consume eventos del dominio");
            accountsContext.Uses(messageBus, "Publica y consume eventos del dominio");
            trainingContext.Uses(messageBus, "Publica y consume eventos del dominio");
            businessContext.Uses(messageBus, "Publica y consume eventos del dominio");
            contentContext.Uses(messageBus, "Publica y consume eventos del dominio");
            digitalContext.Uses(messageBus, "Publica y consume eventos del dominio");
            dataContext.Uses(messageBus, "Publica y consume eventos del dominio");
            identityContext.Uses(messageBus, "Publica y consume eventos del dominio");

            identityContext.Uses(googleSystem, "");
            digitalContext.Uses(googleSystem, "");
            dataContext.Uses(googleSystem, "");
                        
            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            //landingPage.AddTags("LandingPage");            
            apiGateway.AddTags("APIGateway");
            database1.AddTags("Database");
            database3.AddTags("Database");
            database5.AddTags("Database");
            database6.AddTags("Database");
            database2.AddTags("Database");
            database4.AddTags("Database");
            database7.AddTags("Database");
            database8.AddTags("Database");
            database9.AddTags("Database");
            database10.AddTags("Database");
            database11.AddTags("Database");
            sharedContext.AddTags("BoundedContext");            
            ecommerceContext.AddTags("BoundedContext");            
            inventoryContext.AddTags("BoundedContext");
            accountsContext.AddTags("BoundedContext");
            trainingContext.AddTags("BoundedContext");
            businessContext.AddTags("BoundedContext");
            contentContext.AddTags("BoundedContext");
            digitalContext.AddTags("BoundedContext");
            dataContext.AddTags("BoundedContext");
            identityContext.AddTags("BoundedContext");
            messageBus.AddTags("MessageBus");


            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            //styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIGateway") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("BoundedContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MessageBus") { Width = 850, Background = "#fd8208", Color = "#ffffff", Shape = Shape.Pipe, Icon = "" });


            ContainerView containerView = viewSet.CreateContainerView(librarySystem, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements(); 
            
            //3. Diagrama de Componentes -> post
            /*Component domainLayerHistoriasContext = historiasContext.AddComponent("Domain Layer", "Domino del contexto", "Spring Boot(NestJS)");
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
            pagoComponentView.AddAllComponents();  */        

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}