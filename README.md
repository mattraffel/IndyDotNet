# WHAT

This project provides .NET facade for using IndySDK C callable API. 

# WHY

IndySDK project does include a .NET wrapper. The IndySDK DotNet wrapper expects
developers to know the intrisic details of IndySDK, explicitly needing to know the data
contracts.  Not necessarily a bad thing, but _it does make it every unfriendly
and difficult to use._ 

We believe there is a better implementation.  IndyDotNet (this project) is 
truly object oriented and follows SOLID principles.  The benefits (and goals) 
of this implementation are:
- Work with defined types. No need to understand the data contracts and json structure.
- Make calls synchronously or asynchronously as you need with ease. No need to make ever call asynchronous
- Better compatibility between IndySDK versions. Updates in IndySDK API will not break existing code much better than the other wrapper.  


# LICENSE
MIT  
Apache 2.0


# Components of this project
See [Projects.MD](Projects.MD)


# See Also
https://github.com/hyperledger/indy-sdk