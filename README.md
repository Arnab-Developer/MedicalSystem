# Medical system

[![Doctor service CI](https://github.com/Arnab-Developer/medical-system/actions/workflows/doctor-ci.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/doctor-ci.yml)
[![Patient service CI](https://github.com/Arnab-Developer/medical-system/actions/workflows/patient-service-ci.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/patient-service-ci.yml)
[![Consultation service CI](https://github.com/Arnab-Developer/medical-system/actions/workflows/consultation-service-ci.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/consultation-service-ci.yml)
[![Web gateway CI](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-gateway-ci.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-gateway-ci.yml)
[![Web mvc CI](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-mvc-ci.yml/badge.svg)](https://github.com/Arnab-Developer/medical-system/actions/workflows/web-mvc-ci.yml)

Medical system is a small application build with ASP.NET 5 and EF to document medical records. 
User can store doctor, patient and consultation information in this application with their web browser.
This is a proof of concept of how we can use microservice with ASP.NET 5.

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
