# KEDA Demo

This demo application showcases the implementation of [KEDA - Kubernetes Event-driven Autoscaling](https://keda.sh/) using a scaler for an Azure Service Bus Queue. The Keda Demo application uses the following tech stack:
- .NET 6 
- Docker 
- Helm 
- Azure DevOps CI and CD YAML pipeline
- KEDA

## What is KEDA?

KEDA is a Kubernetes event-driven autoscaler that allows you to scale your applications according to events that occur inside or outside of your Kubernetes cluster. It is very easy to install KEDA using a Helm chart, and it also runs on any platform no matter what vendor or cloud provider you use. The community and the KEDA maintainers have created more than 40 built-in scalers that allow scaling on events from sources like Azure Service Bus, Azure Storage Account, Redis Streams, Apache Kafka, or PostgreSQL. Additionally, it provides out-of-the-box integration with environment variables, K8s secrets, and pod identity.

Another neat feature is that KEDA can scale deployments or jobs to 0. Scaling to zero allows you to only spin up containers when certain events occur, for example, when messages are placed in a queue. This is the same behavior as serverless solutions like Azure Functions, but this feature allows you to run Azure Functions outside of Azure.

## Help & Support

I welcome feedback and comments from everyone

***Having trouble?***
- Verify you have followed all written instructions in the blog posts [KEDA - Kubernetes Event-driven Autoscaling](https://www.programmingwithwolfgang.com/keda-kubernetes-event-driven-autoscaling) and [Deploy KEDA and an Autoscaler using Azure DevOps Pipelines](https://www.programmingwithwolfgang.com/deploy-keda-and-autoscaler-using-azure-devops-pipelines).
- Submit an issue with a detailed description of the problem.
- Write a comment on the blog post where you have the problem.

## Further Documentation

To learn how to use KEDA in Kubernetes and how to deploy the demo application, see my blog posts [KEDA - Kubernetes Event-driven Autoscaling](https://www.programmingwithwolfgang.com/keda-kubernetes-event-driven-autoscaling) and [Deploy KEDA and an Autoscaler using Azure DevOps Pipelines](https://www.programmingwithwolfgang.com/deploy-keda-and-autoscaler-using-azure-devops-pipelines).

This repository is also used as demo application for the article "Level Up die Skalierung in Kubernetes mit KEDA" in the Windows Developer Magazin 3/2022.

All applications in this MicroserviceDemo repository are used for the [Microservices - From Zero to Hero](https://www.programmingwithwolfgang.com/microservice-series-from-zero-to-hero) series.

### Useful Links
- [Azure Kubernetes Service - Getting Started](https://www.programmingwithwolfgang.com/azure-kubernetes-service-getting-started)
- [Helm - Getting Started](https://www.programmingwithwolfgang.com/helm-getting-started)
- [Deploy to Kubernetes using Helm Charts](https://programmingwithwolfgang.com/deploy-kubernetes-using-helm)
- [Deploy to Azure Kubernetes Service using Azure DevOps YAML Pipelines](https://programmingwithwolfgang.com/deploy-kubernetes-azure-devops)
- [Auto-scale in Kubernetes using the Horizontal Pod Autoscaler](https://programmingwithwolfgang.com/auto-scale-kubernetes-hpa)
- [Deploy KEDA and an Autoscaler using Azure DevOps Pipelines](https://programmingwithwolfgang.com/deploy-keda-and-autoscaler-using-azure-devops-pipelines)