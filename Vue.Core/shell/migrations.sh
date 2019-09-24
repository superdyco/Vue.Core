#!/usr/bin/env bash
if [ $# -eq 0 ]
then
    echo "No arguments supplied"
    exit 1
fi

if [ "$1" ] 
then 
    dotnet ef migrations add "$1" --verbose --project ../../Vue.Core.Data/Vue.Core.Data.csproj --startup-project ../../Vue.Core/Vue.Core.csproj    
else
    echo "please input add migrations name"    
fi



cmd /k