# hello.signalr

Trying out [SignalR](https://github.com/SignalR/SignalR) across various platforms.

## Build

Run & build the .NET server

    cd net
    build.bat
    ...

Run & build the java client

    cd java
    gradle clean build shadowJar
    java -jar build\libs\hello-signalr-0.0.1-SNAPSHOT-all.jar
    
