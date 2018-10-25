# WHAT

This project provides .NET facade for using IndySDK C callable API. 

# WHY

IndySDK project does include a .NET wrapper. The IndySDK DotNet wrapper expects
developers to know the intrisic details of IndySDK, explicitly needing to know the data
contracts.  Not necessarily a bad thing, but _it does make it every unfriendly
and difficult to use._ 

We believe there is a better implementation.  IndyDotNet (this project) is 
truly object oriented and following SOLID principles (not to mention, 
doesn't reqire every call to be async, only when you need it). 

The purpose of this project it to provide a interface to IndySDK that is 
easy to consume and implement, following SOLID engineering principles. 

# LICENSE
MIT  
Apache 2.0


# Components of this project
See (Projects.MD)[Projects.MD]


# See Also
https://github.com/hyperledger/indy-sdk