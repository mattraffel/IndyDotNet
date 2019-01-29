# Assumptions
This document itself is work in progress.  Right now, its pretty terse and
the makes more sense in my head than anyone else.  Feel free to contact [me](
matt.raffel@evernym.com)

# Current Version supported
Tested and building against Indy-Sdk 1.8.0 (master)

# Link to LibIndy
1. Download and build LibIndy (requires rust) using master for now
2. copy or create symbolic link to libindy.dylib.  it must be called indy for now

# Setting up pools
1. in token-plugin project at [Sovrin](https://github.com/sovrin-foundation/token-plugin) 
either build or run the docker image.  Run command looks like this
```
docker run -itd -p 9701-9708:9701-9708 indy_pool
```

## Alternative to building LibIndy, download stable 1.7.0
Windows - download [binary](https://repo.sovrin.org/windows/libindy/)  
Ubuntu - download [binary](https://repo.sovrin.org/sdk/lib/apt/xenial/stable/)

# another set of instructions for settting up an enviroment
[IndyDev](https://github.com/kdenhartog/indy-dev)