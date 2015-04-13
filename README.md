# ISSRT
2015 space apps challenge submission ([link](https://2015.spaceappschallenge.org/project/iss-real-time/))

Project demo can be found [here](http://fporter2.github.io/ISSRT/)

Video link [here](https://www.youtube.com/watch?v=mbknBLJOr1w)

## Description

This is a 3D html5/webgl application that lets you see the ISS (using live data) and the sun.

## Controls

- Using the mouse, left button, to drag you can orbit around the earth. 
- The mouse scroll wheel or left alt + drag will zoom in and out.
- Left clicking on the sun or ISS icon wil show data for that object and clicking again will dismiss the prompt.

## Build Requirements

- Unity5

## Future Work

- Adding the moon position (and other celestial bodies)
- Showing more of the live data in an interactive way
- Handling Loss of Signal (LoS) for the live data


## Known Issues

- Sometimes the live data may not update because of trying to push data quickly to the application.
- Some users have a hard time finding the ISS, would need to add an indicator or button to navigate there for users.


## License

Licensed under the Apache License, Version 2.0

Copyright 2015 Forrest Porter

This software contains code derived from the
C++ SGP4 Satellite Library (https://www.danrw.com/sgp4/)

Copyright 2013 Daniel Warner <contact@danrw.com>