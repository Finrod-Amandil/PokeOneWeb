#!/bin/bash
#logs can be read using "journalctl -t p1datasync"
echo "Starting datasync" | systemd-cat -p info -t p1datasync
cd /opt/PokeOneWeb.DataSync/
/bin/bash -c "/opt/PokeOneWeb.DataSync/PokeOneWeb.DataSync" | systemd-cat -p info -t p1datasync > /dev/null 2>&1

retVal=${PIPESTATUS[0]}
if [ $retVal -eq 0 ]; then
        sudo chmod 660 -R /opt/PokeOneWeb.Resources/
        sudo rm -rf /opt/PokeOneWeb.Resources/*
        sudo yes | sudo cp -rf /opt/PokeOneWeb.DataSync/resources/* /opt/PokeOneWeb.Resources/
        sudo chmod 555 -R /opt/PokeOneWeb.Resources/
        find /opt/PokeOneWeb.Resources/ -type f | sudo xargs chmod 444
        echo "files copied to the Resources Folder." | systemd-cat -p info -t p1datasync
else
        echo "Sync failed with returnvalue $retVal, no new files where copied to the Resources Folder" | systemd-cat -p warning -t p1datasync
fi
