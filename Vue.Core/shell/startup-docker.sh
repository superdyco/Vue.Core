#!/usr/bin/env bash
cd ../docker
docker-compose -f startup.yml up -d
docker-compose -f nginx.yml up -d --build
