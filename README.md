Added the following objects to the MediatR library for DDD:

- AggregateRoot
- IAggregateRoot
- IEvent
- IEventStore
- IRepository
- Repository
- ValueObject
- AggregateConcurrencyException
- AggregateEventMissingIdException
- AggregateEventOutOfOrderException
- AggregateNotFoundException

### Installing MediatR.DDD

You should install [MediatR.DDD with NuGet](https://www.nuget.org/packages/theCodeReaper.MediatR.DDD/):

    Install-Package theCodeReaper.MediatR.DDD
    
Or via the .NET Core command line interface:

    dotnet add package theCodeReaper.MediatR.DDD