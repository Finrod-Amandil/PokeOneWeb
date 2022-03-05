#wait for the SQL Server to come up
for i in {1..50};
do
    echo "running db generation"
    #run the setup script to create the DB 
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 1Secure*Password1 -d master -i db/pokeonewebreadmodel.sql
    if [ $? -eq 0 ]
    then
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 1Secure*Password1 -d master -i db/pokeoneweb.sql
        echo "finished db generation"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done