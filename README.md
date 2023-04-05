# CQRS-TEMPLATE
.NET TEMPLATE WITH CQRS USING MEDIATOR


[![.NET](https://img.shields.io/static/v1?style=flat&logo=.net&label=.NET&message=7.0.0&logoColor=white&labelColor=512BD4&color=inactive)](https://dotnet.microsoft.com/download/dotnet/7.0)
[![Docker](https://img.shields.io/static/v1?style=flat&logo=Docker&label=Docker&message=20.10.7&logoColor=white&labelColor=2496ED&color=inactive)](https://www.docker.com/)
[![Docker](https://img.shields.io/static/v1?style=flat&logo=Azure&label=Azure&message=20.10.7&logoColor=white&labelColor=2496ED&color=inactive)](https://www.docker.com/)

Template usando mediator con CQRS la aplicacion tiene como finalidad manejar autentication y autorizacion usando JWT.


### Tabla de contenido
- [Unit SSO](#micro-payments)
    - [Tabla de contenido](#tabla-de-contenido)
    - [Especificaciones:](#especificaciones)
    - [Composición:](#composición)
        - [Endpoints](#endpoints)
        - [Modelos](#modelos)
            - [Users](#Users)
            - [Applications](#Applications)
            - [Permissions](#Permissions)
            - [Role](#Role)
            - [PermissionToRoles](#PermissionToRoles)
        - [QueryPagination](#querypagination)
        - [Enums](#enums)
        - [Contribución](#contribución)
            - [Lineamientos](#lineamientos)
            - [Documentación](#documentación)
            - [Unit Testing](#unit-testing)
            
### Especificaciones:
- [Swagger](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/)
- [xUnit](https://www.nuget.org/packages/Xunit)
- [Net7](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Automapper](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection)
- [FluentValidation](https://fluentvalidation.net/)
- [Mediator](https://github.com/mayuanyang/Mediator.Net)
- [EntityFrameWork](https://learn.microsoft.com/en-us/ef/)
- [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Domain Driven Design](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)

### Composición:
1. 📂Unit.Api
   1. ┣ 📂Controllers
   2. ┣ 📂Settings
   3. ┣ Program.cs
2. 📂Unit.Aplication
    1. ┣ 📂AzureADApp
    2. ┣ 📂Users
    3. ┣ 📂Common
    4. ┣ 📂Organization
    5. ┣ IoC.cs
3. 📂Unit.Domain
    1. ┣ 📂Common
4. 📂Unit.Infrastructure
    1. ┣ 📂Context
    2. ┣ 📂Migration
   3. ┣ 📂Policies
   4. ┣ 📂Services
   5. ┣ 📂Utils
   6. ┣ IoC.cs
  
- `Unit.Api`: es el proyecto principal del microservicio.
- `Controllers`: es una carpeta que contiene los controladores de la aplicación.
  `Unit.Aplication`: es el proyecto con los comandos y queries del microservicio.
- `Unit.Domain`: es el proyecto principal del microservicio.
- `Unit.Infrastucture`: es una carpeta que contiene los hubs (sockets con signalR) de la aplicación.
- `Migrations`: es una carpeta que contiene las migraciones de la aplicación.
- `Context`: es una carpeta que contiene los Contextos de la base de datos.
- `Policies`: es una carpeta que contiene los modelos de la aplicación.
- `Services`: es una carpeta que contiene los servicios o logica de negocio de la aplicación.
- `Dtos`: es una carpeta que contiene los DTOs de la aplicación.
- `Interfaces`: es una carpeta que contiene los contratos de los servicio de la aplicación.
- `Tests`: es una carpeta que contiene los tests de la aplicación.

### Endpoints
_Favor revisar documentación en swagger al momento de correr la api para mejor comprensión._

| Ruta                                                                                  | Método   |
|:--------------------------------------------------------------------------------------|:---------| 
| `/api/access-token`                                                                   | `Get`    | 
| `/api/access-token`                                                                   | `Post`   | 
| `/api/access-token/refresh`                                                           | `Post`   | 
| `/api/azure-ad-apps/active-directory`                                                 | `Get`    |              
| `/api/azure-ad-apps`                                                                  | `Get`    | 
| `/api/azure-ad-apps`                                                                  | `POST`   | 
| `/api/azure-ad-apps/{id}`                                                             | `GET`    | 
| `/api/azure-ad-apps/{id}`                                                             | `PUT`    | 
| `/api/azure-ad-apps/{id}`                                                             | `DELETE` | 
| `/api/azure-ad-apps/{organizationId}`                                                 | `DELETE` | 
| `/api/azure-ad-permissions/active-directory`                                          | `Get`    | 
| `/api/azure-ad-permissons/active-directory/{oid}`                                     | `Get`    | 
| `/api/azure-ad-permissons/permissions`                                                | `Get`    | 
| `/api/azure-ad-permissions`                                                           | `POST`   | 
| `/api/azure-ad-permissions/{id}`                                                      | `GET`    | 
| `/api/azure-ad-permissions/{id}`                                                      | `PUT`    | 
| `/api/azure-ad-permissions/{id}`                                                      | `DELETE` | 
| `/api/azure-ad-permissions/category`                                                  | `GET`    | 
| `/api/azure-ad-permissions/categories`                                                | `GET`    |
| `/api/azure-ad-permissions/organization/{organizationId}`                             | `GET`    | 
| `/api/azure-ad-permissions/organization/no-organization`                              | `GET`    | 
| `/api/azure-ad-permissions/categories`                                                | `GET`    | 
| `/api/azure-ad-permissions-to-organizations`                                          | `GET`    | 
| `/api/azure-ad-permissions-to-organizations`                                          | `POST`   | 
| `/api/azure-ad-permissions-to-organizations/{id}`                                     | `DELETE` | 
| `/api/azure-ad-permissions-to-organizations/{organizationId}`                         | `GET`    | 
| `/api/azure-ad-users`                                                                 | `GET`    | 
| `/api/azure-ad-users`                                                                 | `POST`   | 
| `/api/azure-ad-users/{id}`                                                            | `PUT`    | 
| `/api/azure-ad-users/{id}`                                                            | `DELETE` |
| `/api/azure-ad-users/user-role/{roleId}`                                              | `Get`    |
| `/api/azure-ad-users/photo/{userOid}`                                                 | `Get`    |
| `/api/azure-ad-users/organization/{organizationId}`                                   | `Get`    |
| `/api/azure-ad-users/unaffiliated-users/`                                             | `Get`    |
| `/api/azure-ad-users-permissions/{permissionId}`                                      | `GET`    |
| `/api/azure-ad-users-permissions/{userId}`                                            | `GET`    |
| `/api/azure-ad-users-permissions`                                                     | `GET`    |
| `/api/azure-ad-users-permissions`                                                     | `DELETE` |
| `/api/azure-ad-users-permissions`                                                     | `POST`   |
| `/api/organization/{organizationId}`                                                  | `GET`    | 
| `/api/organization`                                                                   | `GET`    | 
| `/api/organization`                                                                   | `POST`   | 
| `/api/organization/overview/{organizationId}`                                         | `GET`    | 
| `/api/organization/admins`                                                            | `POST`   | 
| `/api/organization/remove/admins`                                                     | `PUT`   | 
| `/api/organization/{id}`                                                              | `PUT`    | 
| `/api/organization/{id}`                                                              | `DELETE` |
| `/api/permissions-to-roles`                                                           | `GET`    |
| `/api/permissions-to-roles`                                                           | `POST`   | 
| `/api/permissions-to-roles/{id}`                                                      | `GET`    |
| `/api/permissions-to-roles`                                                           | `DELETE` |
| `/api/role/{id}`                                                                      | `GET`    |
| `/api/role/{id}`                                                                      | `PUT`    |
| `/api/role/{id}`                                                                      | `DELETE` |    
| `/api/role`                                                                           | `GET`    |
| `/api/roles/organization/{organizationId}`                                            | `GET`    |
| `/api/role`                                                                           | `POST`   |
| `/api/role/user`                                                                      | `POST`   |  
| `/api/role/user`                                                                      | `DELETE` | 
| `/api/users/current`                                                                  | `GET`    | 

### Modelos

##### AzureADAppDto
```json
{
  "organizationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "displayName": "string",
  "description": "string",
  "spaRedirect": {
    "redirectUri": [
      "string"
    ]
  },
  "web": {
    "implicitGrantSettings": {
      "enableAccessToken": true,
      "enableIdToken": true
    }
  }
}
```
##### AzureADPermissionDto | Create/Update
```json
{
  "displayName": "string",
  "description": "string",
  "value": "string",
  "categories": 0,
  "organizationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

##### AzureADPermissionDto | Read
```json
{
    "id": "Guid",
    "appPermissionOid": "Guid",
    "appId": "Guid",
    "appOid": "Guid",
    "azureADAppId": "Guid",
    "displayName": "string",
    "description": "string",
    "value": "string"
}
```

##### AzureADPermissionToOrganizationDto
```json
{
  "id": "Guid",
  "azureADPermissionId": "Guid",
  "organizationId": "Guid",
}
```

##### AzureADUserDto
```json
{
  "firstName": "string",
  "fullName": "string",
  "userName": "string",
  "isOrganizationAdmin": false,
  "isEnabled": true,
  "organizationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "roleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "passwordProfile": {
    "password": "string",
    "forceChangePassword": true
  }
}
```
##### Organization
```json
{
  "name": "string",
  "adminsIDs": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ],
  "usersIDs": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ],
  "permissionsIDs": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ],
  "isGlobalOrganization": true
}

```
##### PermissionToRoles
```json
{
  "permissionsId": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ],
  "roleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
##### PermissionToOrganization
```json
{
  "azureADPermissionIds": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ],
  "organizationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}


```
##### Roles
```json
{
  "name": "string",
  "description": "string",
  "status": true,
  "organizationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}

```
##### AccesToken
```json
{
  "code": "string"
}

```
#### Documentación
Un pull request bien documentado debe contar con los siguientes factores:
- Describir de manera precisa su finalidad.
- Cuenta con la(s) tarea(s) que dieron paso a su creación.
- Explica el desarrollo realizado.

#### Unit Testing
Las pruebas unitarias están implementadas utilizando [Xunit](https://www.nuget.org/packages/xunit/). Todo desarrollo realizado debe estar cubierto en un gran porcentaje por pruebas unitarias.

Las pruebas unitarias son fundamentales, por lo que las mismas deben desarrollarse con rigurosidad y teniendo en cuenta que la cobertura de estas se valida 
Para más información puede navegar al siguient [enlace](https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019).
