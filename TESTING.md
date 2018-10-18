# Assumptions
This document itself is work in progress.  Right now, its pretty terse and
the makes more sense in my head than anyone else.  Feel free to contact [me](
matt.raffel@evernym.com)

# Link to LibIndy
1. Download and build LibIndy (requires rust) using master for now
2. copy or create symbolic link to libindy.dylib.  it must be called indy for now

# Setting up pools
1. in token-plugin project at [Sovrin](https://github.com/sovrin-foundation/token-plugin) 
either build or run the docker image.  Run command looks like this
```
docker run -itd -p 9701-9708:9701-9708 indy_pool
```