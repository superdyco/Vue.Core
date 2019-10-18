#!/usr/bin/env bash

echo 'clean publish folder'
cd ../../
pwd
rm -r publish/*

echo 'webpack build'
cd Vue.Core/ClientApp
#npm run build:prod

#run testing
echo 'run testing'
cd ././../../Vue.Core.Tests/
pwd
dotnet test --logger "trx;LogFileName=TestResult.xml"
cd TestResults/
total=$(grep -oP '(?<=total=")[^"]*' TestResult.xml)
passed=$(grep -oP '(?<=passed=")[^"]*' TestResult.xml)
executed=$(grep -oP '(?<=executed=")[^"]*' TestResult.xml)
echo "testing total="${total}
echo "testing executed="${executed}
echo "testing passed="${passed}
if [ ! -z "${total}" ]
then
    if [ ${total} -eq ${passed} ] && [ ${executed} -eq ${passed} ];
    then
        echo "run publish"
        cd ../../
        dotnet publish -r linux-x64 -c Release -o ./publish/ -p:PublishSingleFile=true
        docker build . -f ./Dockerfile -t zlinebot:production
        cd Vue.Core/docker
        docker-compose -f app.yml down
        docker-compose -f app.yml up
        #清除Temp images
        #echo y|docker system prune --all
    else
        echo "testing failed"
    fi
 else
        echo "not found testing data"
fi

cmd /k