# ModemToolkit
ModemToolkit is a self-hosted web service which provides modem information received from gnokii.

### Usage
- Install and configure `Ubuntu`, `Mono`, `Gnokii`, your USB modem, etc.
- Copy binary files of `ModemToolkit.Service` project to `/ModemToolkit/`
- Copy `ModemToolkit.Service/ModemToolkit` file to `/etc/init.d/ModemToolkit` (* tested on Ubuntu 14 LTS only)
- Run
```
sudo update-rc.d ModemToolkit defaults
```
```
sudo update-rc.d ModemToolkit enable
```
- Now you can run the service
```
sudo '/etc/init.d/ModemToolkit' start
```
- Open web page
```
http://127.0.0.1:50248/Api/Modem/GetInfo
```

### Requirements
- Ubuntu 14 LTS
- Mono (.NET 4.5 implementation)
- Gnokii
- USB modem