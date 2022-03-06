#!/bin/bash
#check if db files are already existing
if [[ -f /var/opt/mssql/data/PokeOneWeb.mdf ]] && [[ -f /var/opt/mssql/data/PokeOneWebReadModel.mdf ]]
then
    echo "#####################################################"
    echo "Databases both already exist. Skipping db generation"
    echo "#####################################################"
else
    #wait for the SQL Server to come up (max ~60s)
    for i in {1..60};
    do
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 1Secure*Password1 -Q "SELECT @@VERSION"
        if [ $? -eq 0 ]
        then
            echo "#####################################################"
            echo "running db generation"
            start=`date +%s`
            #run the setup script to create the DB and import its data
            if [[ ! -f /var/opt/mssql/data/PokeOneWebReadModel.mdf ]]
            then
                echo "DB PokeOneWebReadModel is not existing, starting creation..."
                /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 1Secure*Password1 -d master -i db/pokeonewebreadmodel.sql
            fi
            if [[ ! -f /var/opt/mssql/data/PokeOneWeb.mdf ]]
            then
                echo "DB PokeOneWeb is not existing, starting creation..."
                /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 1Secure*Password1 -d master -i db/pokeoneweb.sql
            fi
            end=`date +%s`
            runtime=$((end-start))
            echo "finished db generation in $runtime s"
            echo "#####################################################"
            break
        else
            echo "not ready yet..."
            sleep 1
        fi
    done
fi