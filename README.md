# Medical system

Medical system is a small application build with ASP.NET 5 and EF to document medical records. 
User can store doctor, patient and consultation information in this application with their web browser.
This is a proof of concept of how we can use microservice with ASP.NET 5. 

![image](https://user-images.githubusercontent.com/3396447/113992852-7cf3a200-9871-11eb-8088-5063df947bc7.png)

This is influenced by [.NET Microservices Sample Reference Application](https://github.com/dotnet-architecture/eShopOnContainers)

## CI CD status

| Module | Status |
|--------|-----------|
| Doctor service | [![Doctor service CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/doctor-service-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/doctor-service-ci-cd.yml) |
| Patient service | [![Patient service CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/patient-service-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/patient-service-ci-cd.yml) |
| Consultation service | [![Consultation service CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/consultation-service-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/consultation-service-ci-cd.yml) |
| Web gateway | [![Web gateway CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/web-gateway-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/web-gateway-ci-cd.yml) |
| Web mvc | [![Web mvc CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/web-mvc-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/web-mvc-ci-cd.yml) |
| Doctor sync job | [![Doctor sync job CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/doctor-sync-job-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/doctor-sync-job-ci-cd.yml) |
| Patient sync job | [![Patient sync job CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/patient-sync-job-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/patient-sync-job-ci-cd.yml) |
| Health check dashboard | [![Health check dashboard CI CD](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/health-check-dashboard-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/MedicalSystem/actions/workflows/health-check-dashboard-ci-cd.yml) |

## Tech stack

| Module | Tech |
|--------|------|
| Backend apis | ASP.NET 5 gRPC service |
| Gateway | ASP.NET 5 Web Api |
| Frontend web application | ASP.NET 5 MVC |
| Jobs | Azure function |
| Database access | Entity Framework |
| Database | SQL Server |
| Unit test | NUnit |

## Architecture diagram

![Medical system architecture](https://github.com/Arnab-Developer/MedicalSystem/blob/main/Assets/MedicalSystem.jpg)

## Docker images

This app is in docker images and stored in dockerhub.

| Images | Dockerhub | Version |
|--------|---------|-----------|
| Doctor service | https://hub.docker.com/r/45862391/doctorservice | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/doctorservice) |
| Patient service | https://hub.docker.com/r/45862391/patientservice | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/patientservice) |
| Consultation service | https://hub.docker.com/r/45862391/consultationservice | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/consultationservice) |
| Web gateway | https://hub.docker.com/r/45862391/webgateway | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/webgateway) |
| Web mvc | https://hub.docker.com/r/45862391/webmvc | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/webmvc) |

## Hosting

When a new release is created then this app is stored inside a docker image and push to docker hub 
through CI CD. Hosting in Azure is not done as a part of CI CD. That needs to be done manually.

- Create a new resource group in Azure
- In that resource group create the following resources
  - App Service Plan for Linux
  - Web App for Doctor service, Patient service, Consultation service, Web Gateway and Web Mvc
  - SQL Server
  - Database for Doctor service, Patient service and Consultation service
- Create tables in the databases for Doctor service, Patient service and Consultation service
- Update the connection strings in the web app configuration for Doctor service, Patient service and Consultation service
- Update api endpoint urls in Web Gateway and Web Mvc

Now open the Web App URL in web browser for Web Mvc and you should access the app.

The future plan is to deploy the services in AKS.

## How to run

You need Visual Studio 2019, ASP.NET 5, Entity Framework, Azure tools for Visual Studio 2019 and SQL Server 2016 to work 
on this solution locally. 

- Clone the repo
- Create the databases and tables with the
[script provided](https://github.com/Arnab-Developer/MedicalSystem/tree/main/DatabaseScripts) 
in the repo.
- Update the connection strings and other configs in `appsettings.Development.json` 
file in different projects as applicable to your local settings before executing this app.
- Open the solution in Visual Studio
- Set the below projects as startup projects
  - Doctor service
  - Patient service
  - Consultation service
  - Web gateway
  - Web frontend
- Press F5 to run
