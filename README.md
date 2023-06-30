# Poliza
gestión de pólizas

# Documentación swagger:
[https://localhost:7218/swagger/index.html](https://localhost:7218/swagger/index.html)

# Patrones:
* Mediator
* Command
* Repository

El proyecto fué construido con .NET 6 implementando los patrones mencionados anteriormente sobre un Clean Architecture.
Se utiliza JWT para la generación del token de seguridad además de un endpoint para la creación de usuarios donde el password es cifrado mediante una encriptación de una sola vía.

Se adjunta la colección de Postman con las operaciones implementadas, además al ejecutar el endpoint `/api/auth/login` automáticamente se actualiza el token establecido en las environment del proyecto en postman.
