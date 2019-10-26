#!/usr/bin/env bash
cd ../docker
docker-compose -f startup.yml down
docker-compose -f nginx.yml down
