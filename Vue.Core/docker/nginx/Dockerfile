FROM nginx:latest

RUN mkdir -p /etc/nginx/sites-enabled/ /etc/nginx/ssl
RUN apt-get update && apt-get install -y vim
RUN apt-get install -y nmap
RUN apt-get install -y openssl
COPY nginx.conf /etc/nginx/nginx.conf
COPY sites.conf /etc/nginx/sites-enabled/
# generate crt & key 
RUN openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout /etc/nginx/ssl/nginx.key -out /etc/nginx/ssl/nginx.crt \
    -subj "/C=TW/ST=/L=/O=/OU=/CN=localhost"

CMD ["nginx", "-g", "daemon off;"]