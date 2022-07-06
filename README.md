# Changes
- Added login page.
- Connects to websocket on login.
- On websocket connection lost, 10 attempts are made to reconnect.
- Failure to reconnect sends the user back to login page.
- When the socket disconnects the user because of a bad session token, the user is returned to login.
- Changed getter and setter function convention to properties convention.
- Very simple chunk rendering.
- A whole bunch of other things changed.