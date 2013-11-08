# xDriven #
### A base framework for Xamarin cross-platform solutions. ###

*This framework is under heavy construction currently...*

### Framework - Onion Archetecture
This solution implements [onion architecture](http://www.develop.com/onionarchitecture "Onion Architecture") as the base framework. The reasoning behind this is to allow separation of concern and modularity. It maximizes code reusability and keeps your classes small and manageable. It also gives us the ability to swap out layers with little effort.

This type of archetecture also allows for reuse in other applications needing the same data and model layers. This is especially useful when dealing with enterprise-level applications, as these layers are probably already built (assuming they used the same archetecture), and can be dropped into a Xamarin solution.

### Cross-platform Patterns
* Core.Presenters : Keeps all non-UI related logic in Core (business rules, validation, actions, etc...)
* Core.Context : Persists the application context using the data layer so it can be used in all presentation layers.
* Infrastructure.DependencyResolution : ServiceContainer allows for injection of interfaces (like Ninject)

### Archetecture Overview
*Core*
* *Application* - 
* *Domain* - 

*Infrastructure*
* *IoC* - 
* *SQLite* - 

*Presentation*
* iOS
* Android

*Solution Items*
* README.md
* LICENSE
* .gitignore


## Items on the todo list:
* Create demo iOS presentation layer that shows use of cross-platform code
* Create demo Android presentation layer that shows use of cross-platform code
* Add notification scheduler base to iOS presentation layer
* Add reachability base to iOS presentation layer
* Clean up / repurpose WebRequestService.cs code
* ...a bunch of other stuff... :)
