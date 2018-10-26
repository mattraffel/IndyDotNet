﻿# WHAT

This project provides .NET facade for using IndySDK C callable API. 

# WHY

IndySDK project does include a .NET wrapper. The IndySDK DotNet wrapper expects
developers to know the intrisic details of IndySDK, explicitly needing to know the data
contracts.  Not necessarily a bad thing, but _it does make it every unfriendly
and difficult to use._ 

We believe there is a better implementation.  No offense to all the hard working people
contributing to IndySDK .NET wrapper, but it's just time for something better.  

IndyDotNet (this project) benefits (and goals) of this implementation are:
- Work with defined types. No need to understand the data contracts and json structure.
- Make calls synchronously or asynchronously as you need with ease. 
No need to make ever call asynchronous needlessly.
- Much better isolatation of the IndySDK API resulting in your projects being less fragile.
- Better compatibility between IndySDK versions.
- Better error messages.
- More intutitive CLI implementation.
- DotNetPay serves as an example for implementing specific Indy SDK Payment API.
- For you developers, a true object oriented implementation that follows SOLID principles.

# LICENSE
MIT  
Apache 2.0


# Components of this project
See [PROJECTS.md](PROJECTS.md)


# See Also
https://github.com/hyperledger/indy-sdk