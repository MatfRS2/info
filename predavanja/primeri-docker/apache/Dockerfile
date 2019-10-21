#
# BUILD COMMAND (EXAMPLE)
# docker build -t apache-image .
#
# RUN COMMAND (EXAMPLE)
#  docker run -dit --name apache-container -p 8181:80 apache-image

FROM httpd:2.4
COPY ./public-html/ /usr/local/apache2/htdocs/