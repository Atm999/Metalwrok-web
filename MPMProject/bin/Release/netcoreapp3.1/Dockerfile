#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#引入镜像
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
 
#设定工作目录
WORKDIR /app
 
#在容器中程序使用的端口，一定要和程序启动使用的端口对应上
EXPOSE 80
 
#复制文件到工作目录
COPY . /app
 
#启动程序
ENTRYPOINT ["dotnet", "MPMProject.dll"]