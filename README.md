# WHAT

This project provides .NET facade for using IndySDK C callable API.

# WHY

IndySDK project does include a .NET wrapper. This project is different.

The IndySDK .NET wrapper expects developers to know the intrinsic details of
IndySDK, explicitly needing to know the data contracts.  Not necessarily a bad
thing, but _it does make it very difficult to use._

We believe .NET engineers should work with a library that is much easier to use
and less fragile. This means an idiomatic, object oriented approach over json string data manipulation.

IndyDotNet (this project) benefits (and goals) are:
- Work with defined types. No need to understand the data contracts and json structure.
- Make calls synchronously or asynchronously as you need with ease.
No need to make ever call asynchronous needlessly.
- Much better isolation of the IndySDK API resulting in your projects being less fragile.
- Better compatibility between IndySDK versions.
- Better support on all OSes (depends on IndySDK ability to address OS specific
handling as well, unfortunately)
- Better error messages.
- More intuitive CLI implementation.
- DotNetPay serves as an example for implementing specific Indy SDK Payment API.
- For you developers, a true object oriented implementation that follows SOLID principles.

# Nuget Package
There is now a [nuget package available](https://www.nuget.org/packages/IndyDotNet/):

As of the last update, run this command to install it:
```
dotnet add package IndyDotNet --version 0.1.1
```

# LICENSE
MIT  
Apache 2.0


# Components of this project
See [PROJECTS.md](PROJECTS.md)

# Project Documentation
[PROJECTS.md](PROJECTS.md) - describes the projects that make up this repo  
[TESTING.md](TESTING.md) - how to setup environment for running the tests  
[TODOS.md](TODOS.md) - important engineering topics to be completed

## Acknowledgements
Thank you to the indy-sdk-dotnet wrapper project members for assistance, even if indirectly
helping us.  It would have been much more time consuming if we only had the IndySDK
rust libraries to rely on for information.

# See Also
https://github.com/hyperledger/indy-sdk

# Indy-sdk version
Works with indy-sdk master branch, and version 1.8.0
