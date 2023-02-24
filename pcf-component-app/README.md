# Instructions

In a root directory execute this commands:

```pac pcf init --namespace my.NotificationNamespace --name NotificPCFSignalR --template field```

```npm install @aspnet/signalr --save```

Make new directory "Solutions" and navigate in solutions

``` mkdir Solutions ```

``` cd Solutions ```



Execute this command in solutions to init solution: 

```pac solution init --publisher-name developer --publisher-prefix dev```



Execute this command in solutions to publish solution:
```pac auth create –url https://{yourOrganization}.crm4.dynamics.com```

``` pac pcf push --publisher-prefix dev ```


# Links

https://powerapps.microsoft.com/en-us/blog/notification-control-using-powerapps-component-framework-and-azure-signalr/