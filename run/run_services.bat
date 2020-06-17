cd  ../AuthServer/AuthServer.Host
start dotnet run

cd ../../IdentityService/IdentityService.Host
start dotnet run

cd ../../MicroServices/Business/Business.Host
start dotnet run

cd ../../../Gateways/WebAppGateway/WebAppGateway.Host
start dotnet run