# Web API Security

---

No margin for error

Application developers should understand security

A major part of it is built on application level

Can make the application wildly complex

Security should be considered from the beginning of the development

---

## OWASP Top 10

1. Injection
2. Broken authentication
3. Sensitive data exposure
4. (XML External entities)
5. Broken access control
6. Security misconfiguration
7. (Cross-Site Scripting (XSS))
8. (Insecure deserialization)
9. (Using Components with known vulnerabilities)
10. Insufficient loggin & monitoring

---

## Injection

Can occur when an attacker can send hostile data to an interpreter

Almost any source of data can be an injection vector

Can be protected against by validating, filtering or sanitizing user-supplied data

Note:

There are other more specific ways to protect against injection

---

### Preventing injection attacks in MongoDb

Two main points for prevention

- Properly handling untrusted data
- Avoiding $where operator

We can also disable javascript in the config file:

```config
security:
  javascriptEnabled:false
```

This is how a dangerous statement looks in C#:

```C#
new BsonDocument("$where",
    new BsonJavaScript("function() { return true; }"));
```

---

## Broken authentication

Malicious user can steal identity of another user

Usually the issue is that the attacker can find identities by brute force

Examples of attack types are credential stuffing or dictionary attacks

Can be prevented by making automated attacks not work efficiently, using strong password and password recovery process and handling sessions properly

---

### Authentication in Web API

The first thing the service must do when it receives a new web request is verify the user’s claim of identity

Done by validating the credentials supplied by the user

Caller provides two basic pieces of information:

- Who the user claims to be
- How that claim can be verified

---

### Types of authentication

None

- You don’t need to know the identity of the user, nor do you need to protect any of the service’s resources by applying permissions

Basic

- The caller adds an HTTP authorization header containing a user name and password
- The values are essentially plaintext, using only base64 encoding for simple obfuscation

---

### Types of authentication

Tokens

- Largely used when third-party token issuers are involved
- This relieves your service of the burden of both storing and verifying a user’s credentials

Note:

There are also methods like Kerberos and Certificates but these methods can grow overly complex for public facing APIs

Token based security has become very popular so it’s hard to ignore (have you heard of Oauth?)

Secure Sockets Layer (SSL) transport security should be used to protect the plaintext user name and password
Digest

Provides a fancier method of putting the user name and password in the HTTP header that provides encryption for those values

This is intended to avoid the need for HTTPS

---

### Issues with Basic and Digest authentication

Secrets needs to be passed on every request

Secrets need to be stored in clear text or in reversible encryption or obtained for every request

Server has to validate every request

---

### Tokens

User’s credentials are validated by a 3rd party

Your service doesn’t even receive the credentials, but a token which was created by a trusted issuer

Because your service trusts the token issuer, you can merely accept the token as it is

---

### Tokens

In the context of securing a service, a token:

- Identifies the user
- Contains contents that can be trusted (signed)
- Can be used in place of credentials (user name and password)
- Can contain other attributes (claims)

Note:

- Web users typically have several existing accounts for popular web services and social media websites
- A driver’s license is a kind of a token
- First, your driver’s license is used to identify you. Pretty simple! Next, most people checking your driver’s licensewill trust that it is valid. This is because the license is issued by a government entity and is usually signed in somefashion (e.g., water mark). And because it is trusted, the person checking your license doesn’t need to ask for apassword or PIN or any other proof of identity. And finally, your license includes other attributes beyond justyour identity.
- In short, a token is just a pile of claims that happens to be signed by a trusted issuer, thus removing the needfor your service to store user names and passwords. This is huge, of course, as not having to store passwords greatlyreduces the risk of your users’ passwords being exposed.

---

# Part 2

---

## Sensitive data exposure

Data can be vulnerable in two phases: in transit and at rest

Sensitive data should be protected in both of the phases

You should always be concerned to send anything in plain text (especially using HTTP, SMTP and FTP protocols)

Encrypt all sensitive data at rest with up-to-date and strong algorithms

Don't store sensitive data unnecessarily

Note:

Sensitive data contains also all personally identifiable data (because of GDPR)

---

### Transport Layer Security (TLS) & Secure Sockets Layer (SSL)

Protocols that provide communications security over a network

Most of the security features don't make sense without TLS/SSL

HTTP does not have any security built in

Many app stores demand the use of TLS/SSL

---

### How SSL/TLS works

Client requests a secure resource

The web server sends its public key with its certificate

The client checks that the certificate was:

- issued by a trusted party (usually a trusted root CA)
- certificate is still valid
- certificate is related to the server contacted

The client then uses the public key to encrypt a random symmetric encryption key and sends it to the server with the encrypted URL required as well as other encrypted data

---

### How SSL/TLS works

The server decrypts the symmetric encryption key using its private key and uses the symmetric key to decrypt the URL and data

The server sends back the requested data encrypted with the symmetric key

The client decrypts the data using the symmetric key and displays the information

---

### Encryption on MongoDb

Encrypting the traffic can be done on the application, on the database, or on both

MongoDB supports TLS/SSL (Transport Layer Security/Secure Sockets Layer) to encrypt all of MongoDB’s network traffic

MongoDB can use any valid SSL certificate issued by a certificate authority or a self-signed certificate

Enterprise version of MongoDB also supports encrypting the data

---

## Broken access control

Access control enforces that users cannot act outside their intended permissions

Impact is attackers acting as users using privileged functions, or creating, accessing, updating or deleting every record in the database

Can be taken advantage of by bypassing access control checks by modifying the URL, internal application state, or the HTML page, or simply using a custom API attack tool.

---

### Access control / Authrorization in Web API

Once user has been securely identified, permissions for that user can be enforced

Common way to model these permission is in the form of claims:

![claims](/resources/claims.png)

Note:

- Claims are key-value pairs which provide all kinds of information about the user
- Claim types can have more than one value
- Claims are not limited to only dealing with authorization

---

### Authrorization flow in Web API

A web request arrives that includes an HTTP authorization header containing information about the user and how that information can be verified

The service verifies the user information

The service sets up a security principal object on the current HTTP context that contains the current user’s identity and associated claims

All “downstream” code checks the current context’s principal to determine if/how processing is allowed to continue

Note:

If processing is not allowed to continue, the service will communicate this to the caller via a response message containing the appropriate HTTP status code (i.e. 401 - Unauthorized)

---

## Security misconfiguration

There can be missing security hardening across any part of the application stack or in cloud services

This can mean things anything from having unnecessary features enabled or installed (e.g.unnecessary ports, services, pages, accounts, or privileges) to not using secure settings in frameworks, databases, libraries etc.

Note:

Error handling reveals stack traces or other overly informative error messages to users.

---

### Preventing security misconfiguration

- Configuration should be automated
- There should be an environment that matches as closely as possible the production environment
- Dependencies should be kept at minimum level

---

## Insufficient logging & monitoring

Attackers can rely on the lack of monitoring and timely response to achieve their goals

Auditable events, such as logins, failed logins, and high-value transactions should be logged

There should be automated alerts

---

### Auditing user actions

Identifying the user and storing critical user actions for further investigation

Audit information is usually stored in a database

In Web API auditing can be implemented with an action filter

---
