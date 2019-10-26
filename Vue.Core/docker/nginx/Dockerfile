FROM nginx:latest

RUN mkdir -p /etc/nginx/sites-enabled/
RUN apt-get update && apt-get install -y vim
RUN apt-get install -y nmap
RUN apt-get install -y 
COPY nginx.conf /etc/nginx/nginx.conf
COPY sites.conf /etc/nginx/sites-enabled/


CMD ["nginx", "-g", "daemon off;"]