This repo is used for my 66 blog post series "Microservices - From Zero to Hero". It started out as .NET Core 3.1 project and has been upgraded to .NET 6 since then. The following tools and technologies are used:

- .NET 6
- Swagger
- Asynchronous messaging using RabbitMQ and Azure Service Bus
- Azure Functions
- CI/CD YAML pipelines
- Docker
- Helm
- Kubernetes
- KEDA
- AAD authentication

For more details, see the corresponding blog posts.

## Getting Started with Microservices

The following posts explain the theory behind microservices and how to set up your first two .NET 6 (originally .NET Core 3.1) microservices. Both microservices use the mediator pattern and communicate via RabbitMQ.

- [Microservices - Getting Started](/microservices-getting-started)

- [Programming a Microservice with .NET Core 3.1](/programming-microservices-net-core-3-1)

- [Document your Microservice with Swagger](/document-your-microservice-with-swagger)

- [CQRS in ASP .NET Core 3.1](/cqrs-in-asp-net-core-3-1)

- [Mediator Pattern in ASP .NET Core 3.1](/mediator-pattern-in-asp-net-core-3-1)

- [RabbitMQ in an ASP .NET Core 3.1 Microservice](/rabbitmq-in-an-asp-net-core-3-1-microservice)

- [Dockerize an ASP .NET Core Microservice and RabbitMQ](/dockerize-an-asp-net-core-microservice-and-rabbitmq)

- [Set up Docker-Compose for ASP .NET Core 3.1 Microservices](/set-up-docker-compose-for-asp-net-core-3-1-microservices)

- [Upgrade a Microservice from .NET Core 3.1 to .NET 5.0](/upgrade-microservice-net-core-3-1-net-5-0)

- [Upgrade a Microservice from .NET 5.0 to .NET 6.0](/upgrade-microservice-from-net-5-to-net-6)

## Continuous Integration and Unit Tests in Azure DevOps

This section is all about automated builds using YAML pipelines in Azure DevOps. Starting with a simple .NET Core pipeline, the pipeline switches to building Docker images and then runs xUnit tests inside the Docker container. Another topic of this section is automated versioning of Docker images and lastly splitting up the pipeline into smaller chunks using templates. 

- [Build .NET Core in a CI Pipeline in Azure DevOps](/build-net-core-in-ci-pipeline-in-azure-devops)

- [Run the CI Pipeline during a Pull Request](/run-the-ci-pipeline-during-pull-request)

- [Build Docker in an Azure DevOps CI Pipeline](/build-docker-azure-devops-ci-pipeline)

- [Run xUnit Tests inside Docker during an Azure DevOps CI Build](/run-xUnit-inside-docker-during-ci-build)

- [Get xUnit Code Coverage from Docker](/get-xunit-code-coverage-from-docker)

- [Fixing Broken Unit Test Execution in Dockerfile](/fixing-broken-unit-test-execution-in-dockerfile)

- [Automatically Version Docker Containers in Azure DevOps CI](/automatically-version-docker-container)

- [Improve Azure DevOps YAML Pipelines with Templates](/improve-azure-devops-pipelines-templates)

- [Split up the CI/CD Pipeline into two Pipelines](/split-up-the-ci-cd-pipeline-into-two-pipelines)

## Continuous Deployment with Azure DevOps

After building the Docker images, let's focus on deploying these images to Kubernetes. Each pull request gets deployed into a new namespace, SSL certificates, and a unique URL is generated and Helm is used to manage the deployment. Using Helm allows overriding configuration values. Furthermore, this section explains how to deploy Azure Functions, and SQL Databases and push NuGet packages to an internal or public feed.

- [Approvals for YAML Pipelines in Azure DevOps](/deployment-approvals-yaml-pipeline)

- [Deploy to Azure Kubernetes Service using Azure DevOps YAML Pipelines](/deploy-kubernetes-azure-devops)

- [Improve Azure DevOps YAML Pipelines with Templates](/improve-azure-devops-pipelines-templates)

- [Replace Helm Chart Variables in your CI/CD Pipeline with Tokenizer](/replace-helm-variables-tokenizer)

- [Split up the CI/CD Pipeline into two Pipelines](/split-up-the-ci-cd-pipeline-into-two-pipelines)

- [Deploy Microservices to multiple Environments using Azure DevOps](/deploy-microservices-to-multiple-environments-azure-devops)

- [Deploy every Pull Request into a dedicated Namespace in Kubernetes](/deploy-every-pull-request-into-dedicated-namespace-in-kubernetes)

- [Automatically set Azure Service Bus Queue Connection Strings during the Deployment](/automatically-set-service-bus-queue-connection-string-during-deployment)

- [Deploy Azure Functions with Azure DevOps YAML Pipelines](/deploy-azure-functions-azure-devops-pipelines)

- [Deploy a Docker Container to Azure Functions using an Azure DevOps YAML Pipeline](/deploy-docker-container-azure-functions)

- [Publish NuGet Packages to Nuget.org using Azure DevOps Pipelines](/azure-devops-publish-nuget)

- [Automatically Deploy your Database with Dacpac Packages using Linux and Azure DevOps](/deploy-dacpac-linux-azure-devops)

- [Deploy KEDA and an Autoscaler using Azure DevOps Pipelines](/deploy-keda-and-autoscaler-using-azure-devops-pipelines)

- [Create Custom Roles for Azure DevOps in Azure](/create-custom-roles-for-azure-devops-in-azure)

- [Update DNS Records in an Azure DevOps Pipeline](/update-dns-records-in-an-azure-devops-pipeline)

## Kubernetes with Helm

The following posts explain Microsoft's Azure Kubernetes Service and why Helm is useful for your deployments. After the basics, more advanced topics like Ingress controller, automated SSL certificate installation, and KEDA are discussed.

- [Azure Kubernetes Service - Getting Started](/azure-kubernetes-service-getting-started)

- [Helm - Getting Started](/helm-getting-started)

- [Deploy to Kubernetes using Helm Charts](/deploy-kubernetes-using-helm)

- [Override Appsettings in Kubernetes](/override-appsettings-in-kubernetes)

- [Run a Kubernetes Cluster locally](/run-kubernetes-cluster-locally)

- [Manage Resources in Kubernetes](/manage-resources-kubernetes)

- [Auto-scale in Kubernetes using the Horizontal Pod Autoscaler](/auto-scale-kubernetes-hpa)

- [Readiness and Liveness Probes in Kubernetes](/readiness-health-probes-kubernetes)

- [Use Azure Container Registry in Kubernetes](/azure-container-registry-kubernetes)

- [Automatically scale your AKS Cluster](/automatically-scale-your-aks-cluster)

- [Debug Microservices running inside a Kubernetes Cluster with Bridge to Kubernetes](/debug-microservices-bridge-kubernetes)

- [Set up Nginx as Ingress Controller in Kubernetes](/setup-nginx-ingress-controller-kubernetes)

- [Configure custom URLs to access Microservices running in Kubernetes](/configure-custom-urls-to-access-microservices-running-in-kubernetes)

- [Automatically issue SSL Certificates and use SSL Termination in Kubernetes](/automatically-issue-ssl-certificates-and-use-ssl-termination-in-kubernetes)

- [KEDA - Kubernetes Event-driven Autoscaling](/keda-kubernetes-event-driven-autoscaling)

## SSL Configuration in Kubernetes

Kubernetes helps to automatically create Let's Encrypt SSL certificates and Nginx as Ingress controller allows the creation of unique URLs for each microservice. 

- [Set up Nginx as Ingress Controller in Kubernetes](/setup-nginx-ingress-controller-kubernetes)

- [Configure custom URLs to access Microservices running in Kubernetes](/configure-custom-urls-to-access-microservices-running-in-kubernetes)

- [Automatically issue SSL Certificates and use SSL Termination in Kubernetes](/automatically-issue-ssl-certificates-and-use-ssl-termination-in-kubernetes)

## Create NuGet Packages

NuGet packages allow sharing of code between microservices. Additionally, the versioning of these packages gives developers full control over the version they are using and when they want to upgrade to newer versions.

- [Create NuGet Packages in Azure DevOps Pipelines](/create-nuget-azure-devops)

- [Publish to an Internal NuGet Feed in Azure DevOps](/publish-internal-nuget-feed)

- [Restore NuGet Packages from a Private Feed when building Docker Containers](/restore-nuget-inside-docker)

- [Publish NuGet Packages to Nuget.org using Azure DevOps Pipelines](/azure-devops-publish-nuget)

## Database Deployments with Azure DevOps

Deploying database changes has always been a pain. Using dacpac packages allows developers or database administrators to easily deploy their changes to an existing database, or create a new one with a pre-defined schema and optionally with test data. Docker also comes in handy when trying to deploy the dacpac package using Linux. Since Azure DevOps doesn't support deploying dacpacs on Linux, a custom Docker container is used to deploy the dacpac.

- [Automate Database Deployments](/automate-database-deployments)

- [Automatically deploy Database Changes with SSDT](/automatically-deploy-database-changes)

- [Automatically Deploy your Database with Dacpac Packages using Linux and Azure DevOps](/deploy-dacpac-linux-azure-devops)

- [Use a Database with a Microservice running in Kubernetes](/microservice-with-database-kubernetes)

## Azure Container Registry and Azure Service Bus

The Azure Container Registry is a private repository in Azure. Since it is private, Kubernetes needs an Image pull secret to be able to download images from there. Additionally, this section shows how to replace RabbitMQ with Azure Service Bus Queues and how to replace the .NET background process with Azure Functions to process messages in these queues.

- [Use Azure Container Registry in Kubernetes](/azure-container-registry-kubernetes)

- [Replace RabbitMQ with Azure Service Bus Queues](/replace-rabbitmq-azure-service-bus-queue)

- [Use Azure Functions to Process Queue Messages](/azure-functions-process-queue-messages)

## Azure Functions

Azure Functions can be used to process messages from queues and can be deployed as a Docker container or as .NET 6 application.

- [Deploy Azure Functions with Azure DevOps YAML Pipelines](/deploy-azure-functions-azure-devops-pipelines)

- [Deploy a Docker Container to Azure Functions using an Azure DevOps YAML Pipeline](/deploy-docker-container-azure-functions)

## Infrastructure as Code, Monitoring, and Logging

Infrastructure as Code (IaC) allows developers to define the infrastructure and all its dependencies as code. These configurations are often stored in YAML files and have the advantage that they can be checked into version control and also can be deployed quickly using Azure DevOps. Another aspect of operating a Kubernetes infrastructure is logging and monitoring with tools such as Loki or Prometheus.

- [Use Infrastructure as Code to deploy your Infrastructure with Azure DevOps](/use-infrastructure-as-code-to-deploy-infrastructure)

- [Collect and Query your Kubernetes Cluster Logs with Grafana Loki](/collect-and-query-kubernetes-logs-with-grafana-loki)

- [Monitor .NET Microservices in Kubernetes with Prometheus](/monitor-net-microservices-with-prometheus)

- [Create Grafana Dashboards with Prometheus Metrics](/create-grafana-dashboards-with-prometheus-metrics)

## Service Mesh

Big Kubernetes clusters can be hard to manage. Service Mesh like Istio helps administrators to manage Kubernetes clusters with topics such as SSL connections, monitoring, or tracing. All that can be achieved without any changes to the existing applications. Isitio also comes with a bunch of add-ons such as Grafana, Kiali, and Jaeger to help administrate the cluster.

- [Service Mesh in Kubernetes - Getting Started](/service-mesh-kubernetes-getting-started)

- [Istio in Kubernetes - Getting Started](/istio-getting-started)

- [Use Istio to manage your Microservices](/use-istio-to-manage-your-microservices)

- [Add Istio to an existing Microservice in Kubernetes](/add-Istio-to-existing-microservice-in-kubernetes)

## KEDA - Kubernetes Event-Driven Autoscaling 

Applications have become more and more complicated over the years and often rely on external dependencies these days. These dependencies could be an Azure Service Bus Queue or a database. KEDA allows applications to scale according to these dependencies.

- [KEDA - Kubernetes Event-driven Autoscaling](/keda-kubernetes-event-driven-autoscaling)

- [Deploy KEDA and an Autoscaler using Azure DevOps Pipelines](/deploy-keda-and-autoscaler-using-azure-devops-pipelines)

## AAD Authentication

Using Azure Active Directory authentication allows users to authenticate their applications using Azure identities. The advantage of this approach is that no passwords need to be stored and managed for the connection.

- [Use AAD Authentication for Pods running in AKS](/use-aad-authentication-for-pods-running-in-aks)

- [Implement AAD Authentication to access Azure SQL Databases](/implement-aad-authentication-to-access-azure-sql-databases)

- [Use AAD Authentication for Applications running in AKS to access Azure SQL Databases](/aad-authentication-for-applications-running-in-aks-to-access-azure-sql-databases)

## All Posts in Chronological Order

The following list consists of all blog posts in chronological order:

- [Microservices - Getting Started](/microservices-getting-started)

- [Programming a Microservice with .NET Core 3.1](/programming-microservices-net-core-3-1)

- [Document your Microservice with Swagger](/document-your-microservice-with-swagger)

- [CQRS in ASP .NET Core 3.1](/cqrs-in-asp-net-core-3-1)

- [Mediator Pattern in ASP .NET Core 3.1](/mediator-pattern-in-asp-net-core-3-1)

- [RabbitMQ in an ASP .NET Core 3.1 Microservice](/rabbitmq-in-an-asp-net-core-3-1-microservice)

- [Dockerize an ASP .NET Core Microservice and RabbitMQ](/dockerize-an-asp-net-core-microservice-and-rabbitmq)

- [Set up Docker-Compose for ASP .NET Core 3.1 Microservices](/set-up-docker-compose-for-asp-net-core-3-1-microservices)

- [Build .NET Core in a CI Pipeline in Azure DevOps](/build-net-core-in-ci-pipeline-in-azure-devops)

- [Run the CI Pipeline during a Pull Request](/run-the-ci-pipeline-during-pull-request)

- [Build Docker in an Azure DevOps CI Pipeline](/build-docker-azure-devops-ci-pipeline)

- [Run xUnit Tests inside Docker during an Azure DevOps CI Build](/run-xUnit-inside-docker-during-ci-build)

- [Upgrade a Microservice from .NET Core 3.1 to .NET 5.0](/upgrade-microservice-net-core-3-1-net-5-0)

- [Get xUnit Code Coverage from Docker](/get-xunit-code-coverage-from-docker)

- [Azure Kubernetes Service - Getting Started](/azure-kubernetes-service-getting-started)

- [Helm - Getting Started](/helm-getting-started)

- [Deploy to Kubernetes using Helm Charts](/deploy-kubernetes-using-helm)

- [Override Appsettings in Kubernetes](/override-appsettings-in-kubernetes)

- [Run a Kubernetes Cluster locally](/run-kubernetes-cluster-locally)

- [Automatically Version Docker Containers in Azure DevOps CI](/automatically-version-docker-container)

- [Create NuGet Packages in Azure DevOps Pipelines](/create-nuget-azure-devops)

- [Publish to an Internal NuGet Feed in Azure DevOps](/publish-internal-nuget-feed)

- [Restore NuGet Packages from a Private Feed when building Docker Containers](/restore-nuget-inside-docker)

- [Publish NuGet Packages to Nuget.org using Azure DevOps Pipelines](/azure-devops-publish-nuget)

- [Approvals for YAML Pipelines in Azure DevOps](/deployment-approvals-yaml-pipeline)

- [Deploy to Azure Kubernetes Service using Azure DevOps YAML Pipelines](/deploy-kubernetes-azure-devops)

- [Improve Azure DevOps YAML Pipelines with Templates](/improve-azure-devops-pipelines-templates)

- [Manage Resources in Kubernetes](/manage-resources-kubernetes)

- [Auto-scale in Kubernetes using the Horizontal Pod Autoscaler](/auto-scale-kubernetes-hpa)

- [Replace Helm Chart Variables in your CI/CD Pipeline with Tokenizer](/replace-helm-variables-tokenizer)

- [Automate Database Deployments](/automate-database-deployments)

- [Automatically deploy Database Changes with SSDT](/automatically-deploy-database-changes)

- [Automatically Deploy your Database with Dacpac Packages using Linux and Azure DevOps](/deploy-dacpac-linux-azure-devops)

- [Use a Database with a Microservice running in Kubernetes](/microservice-with-database-kubernetes)

- [Readiness and Liveness Probes in Kubernetes](/readiness-health-probes-kubernetes)

- [Use Azure Container Registry in Kubernetes](/azure-container-registry-kubernetes)

- [Replace RabbitMQ with Azure Service Bus Queues](/replace-rabbitmq-azure-service-bus-queue)

- [Use Azure Functions to Process Queue Messages](/azure-functions-process-queue-messages)

- [Deploy Azure Functions with Azure DevOps YAML Pipelines](/deploy-azure-functions-azure-devops-pipelines)

- [Deploy a Docker Container to Azure Functions using an Azure DevOps YAML Pipeline](/deploy-docker-container-azure-functions)

- [Set up Nginx as Ingress Controller in Kubernetes](/setup-nginx-ingress-controller-kubernetes)

- [Configure custom URLs to access Microservices running in Kubernetes](/configure-custom-urls-to-access-microservices-running-in-kubernetes)

- [Automatically issue SSL Certificates and use SSL Termination in Kubernetes](/automatically-issue-ssl-certificates-and-use-ssl-termination-in-kubernetes)

- [Split up the CI/CD Pipeline into two Pipelines](/split-up-the-ci-cd-pipeline-into-two-pipelines)

- [Deploy Microservices to multiple Environments using Azure DevOps](/deploy-microservices-to-multiple-environments-azure-devops)

- [Deploy every Pull Request into a dedicated Namespace in Kubernetes](/deploy-every-pull-request-into-dedicated-namespace-in-kubernetes)

- [Use Infrastructure as Code to deploy your Infrastructure with Azure DevOps](/use-infrastructure-as-code-to-deploy-infrastructure)

- [Debug Microservices running inside a Kubernetes Cluster with Bridge to Kubernetes](/debug-microservices-bridge-kubernetes)

- [Collect and Query your Kubernetes Cluster Logs with Grafana Loki](/collect-and-query-kubernetes-logs-with-grafana-loki)

- [Monitor .NET Microservices in Kubernetes with Prometheus](/monitor-net-microservices-with-prometheus)

- [Create Grafana Dashboards with Prometheus Metrics](/create-grafana-dashboards-with-prometheus-metrics)

- [Service Mesh in Kubernetes - Getting Started](/service-mesh-kubernetes-getting-started)

- [Istio in Kubernetes - Getting Started](/istio-getting-started)

- [Use Istio to manage your Microservices](/use-istio-to-manage-your-microservices)

- [Add Istio to an existing Microservice in Kubernetes](/add-Istio-to-existing-microservice-in-kubernetes)

- [KEDA - Kubernetes Event-driven Autoscaling](/keda-kubernetes-event-driven-autoscaling)

- [Deploy KEDA and an Autoscaler using Azure DevOps Pipelines](/deploy-keda-and-autoscaler-using-azure-devops-pipelines)

- [Create Custom Roles for Azure DevOps in Azure](/create-custom-roles-for-azure-devops-in-azure)

- [Update DNS Records in an Azure DevOps Pipeline](/update-dns-records-in-an-azure-devops-pipeline)

- [Automatically scale your AKS Cluster](/automatically-scale-your-aks-cluster)

- [Use AAD Authentication for Pods running in AKS](/use-aad-authentication-for-pods-running-in-aks)

- [Implement AAD Authentication to access Azure SQL Databases](/implement-aad-authentication-to-access-azure-sql-databases)

- [Automatically set Azure Service Bus Queue Connection Strings during the Deployment](/automatically-set-service-bus-queue-connection-string-during-deployment)

- [Upgrade a Microservice from .NET 5.0 to .NET 6.0](/upgrade-microservice-from-net-5-to-net-6)

- [Use AAD Authentication for Applications running in AKS to access Azure SQL Databases](/aad-authentication-for-applications-running-in-aks-to-access-azure-sql-databases)

- [Fixing Broken Unit Test Execution in Dockerfile](/fixing-broken-unit-test-execution-in-dockerfile)