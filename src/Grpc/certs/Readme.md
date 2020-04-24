## Certificate Authority
### Create CA key:
openssl genrsa -passout pass:1111 -des3 -out ca.key 4096

### Create CA certificate:
openssl req -passin pass:1111 -new -x509 -days 365 -key ca.key -out ca.crt

# Server
### Create server key:
openssl genrsa -passout pass:1111 -des3 -out server.key 4096

### Create server signing request:
openssl req -passin pass:1111 -new -key server.key -out server.csr

### Sign server certificate w/ CA
openssl x509 -req -passin pass:1111 -days 365 -in server.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out server.crt

### Remove passphrase from server key:
openssl rsa -passin pass:1111 -in server.key -out server.key

# Client
### Create client key
openssl genrsa -passout pass:1111 -des3 -out client.key 4096

### Create client signing request:
openssl req -passin pass:1111 -new -key client.key -out client.csr

### Sign client certificate w/ CA
openssl x509 -passin pass:1111 -req -days 365 -in client.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out client.crt

### Remove passphrase from client key:
openssl rsa -passin pass:1111 -in client.key -out client.key

### Combine private key and certificate in a PFX file
openssl pkcs12 -export -out server.pfx -inkey server.key -in server.crt