## ASP.NET Core 3.1 Service Demos

| Demo                   | Features                                                 |
| ---------------------- | -------------------------------------------------------- |
| ProductsApiDemo        | REST CRUD, Swagger, API Analyzers                        |
| ProductsODataApiDemo   | OData API Controller, CRUD operations                    |
| ProductsApiServiceDemo | Service Discovery, Health Checks, Consul, Docker compose |
| ProductsGrpcDemo       | gRPC, CRUD, Protocol Buffers, Prometheus, Docker compose |

### Extras

The project file also contains some custom tasks for Visual Studio Code that help with making changes to the local dev environment. They'll accessible via the command pallete's "Task: Run Task" command.

- ProductsGrpcClient run
- docker: launch consul
- docker-comppse: Scale Product Service
