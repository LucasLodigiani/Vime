# Vime

Este es un repositorio experimental para practicar Angular y programación en tiempo real, y forma parte del Trabajo Practico final del curso de Angular del Polo Tecnológico Rosario.

Consta de una aplicacion donde los usuarios pueden crear salas de video con un chat integrado, para ello se emplea una api escrita .NET 8 con SignalR(para manejar WebSockets) y un front-end en Angular 18.
La idea general es que el usuario que crea una sala pueda insertar un video y replicar sus acciones como reproducir, adelantar, pausar a los demas usuarios.

To Do:
- [X] Implementar el chat entre usuarios de una misma sala.
- [ ] Agregar validaciones a los formularios del frontend.
- [X] Crear guarda de autenticación.
- [ ] Replicar eventos del reproductor de video en el resto de usuarios conectados a la sala (en progreso).
- [ ] Mostrar errores al usuario.
- [ ] Permitir providers distintos a youtube.




## Guía de instalación para Ubuntu
Instalar SDK de .NET 8
```
wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

sudo apt update

sudo apt install dotnet-sdk-8.0
```
Clonar repositorio
```
https://github.com/LucasLodigiani/Vime.git
```
Correr la api
```
cd Vime/src/Vime.Server
dotnet run
```
Correr el front-end
```
cd Vime/src/vime.client
ng serve
```
