# Medical system
Medical system is a small application build with ASP.NET 5 and EF to document medical records. 
User can store doctor, patient and consultation information in this application with their web browser.
This is a proof of concept of how we can use microservice with ASP.NET 5. 

## CI CD status
| Module | Status |
|--------|-----------|
| Doctor service | [![Doctor service CI CD](https://github.com/Arnab-Developer/medical-system/actions/workflows/doctor-service-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/doctor-service-ci-cd.yml) |
| Patient service | [![Patient service CI CD](https://github.com/Arnab-Developer/medical-system/actions/workflows/patient-service-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/patient-service-ci-cd.yml) |
| Consultation service | [![Consultation service CI CD](https://github.com/Arnab-Developer/medical-system/actions/workflows/consultation-service-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/consultation-service-ci-cd.yml) |
| Web gateway | [![Web gateway CI CD](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-gateway-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-gateway-ci-cd.yml) |
| Web mvc | [![Web mvc CI CD](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-mvc-ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-mvc-ci-cd.yml) |

## Tech stack
| Module | Tech |
|--------|------|
| Backend apis | ASP.NET 5 gRPC service |
| Gateway | ASP.NET 5 Web Api |
| Frontend web application | ASP.NET 5 MVC |
| Jobs | Azure function |
| Database access | EF |
| Database | SQL Server |
| Unit test | NUnit |

## Architecture diagram

![Medical system architecture](https://github.com/Arnab-Developer/medical-system/blob/master/Medical%20system%20architecture.jpg)

## Dockerhub images
| Images | Dockerhub |
|--------|-----------|
| Doctor service | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/doctorservice) |
| Patient service | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/patientservice) |
| Consultation service | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/consultationservice) |
| Web gateway | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/webgateway) |
| Web mvc | ![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/webmvc) |