![ci workflow](https://github.com/SherApp/Sher/actions/workflows/ci.yml/badge.svg) [![codecov](https://codecov.io/gh/SherApp/Sher/branch/main/graph/badge.svg?token=UGSWHXITPW)](https://codecov.io/gh/SherApp/Sher) [![time tracker](https://wakatime.com/badge/github/SherApp/Sher.svg)](https://wakatime.com/badge/github/SherApp/Sher)
# Sher
Simple file sharing system made with a clean architecture in mind

## Available frontends
![default Sher frontend](https://user-images.githubusercontent.com/12448522/128311281-9143c2df-4cea-4304-9bec-d5cbdc31a6ef.gif)
[sher-frontend](https://github.com/SherApp/sher-frontend)

<img src="https://user-images.githubusercontent.com/12448522/128312794-bfe7c3cd-a830-4f28-952b-81acbf667528.jpg" width="500" alt="Sher mobile app screenshot" />

[sher-mobile](https://github.com/SherApp/sher-mobile)

## How to run
### Deploy to a remote server via SSH using docker-compose
1. `git clone https://github.com/SherApp/Sher`
2. cd `Sher`
3. Set the following environment variables (* – optional)

   | Variable | Explanation |
   | -------- | ----- |
   | JwtOptions__SecurityKey | JWT security key (suggested 512 bits) |
   | JwtOptions__Audience | The value of audience claim, can be public domain URL |
   | JwtOptions__Issuer | Issuer of the token, can be API url |
   | PasswordHashingOptions__Iterations* | https://www.twelve21.io/how-to-choose-the-right-parameters-for-argon2/ |
   | PasswordHashingOptions__DegreeOfParallelism* | https://www.twelve21.io/how-to-choose-the-right-parameters-for-argon2/ |
   | PasswordHashingOptions__MemorySize* | https://www.twelve21.io/how-to-choose-the-right-parameters-for-argon2/ |
   | POSTGRES_USER | The username of postgres user |
   | POSTGRES_PASSWORD | The password of postgres user |
   | ConnectionStrings__Default | Postgres connection string. For example, given POSTGRES_USER=sher and POSTGRES_PASSWORD=xsaJSlq8 the connection string would be Username=sher;Password=xsaJSlq8;Server=postgres;Database=sher |
   | Admin__EmailAddress | Email address of the admin account |
   | Admin__Password | Password of the admin account |
4. `DOCKER_HOST=ssh://name@server docker-compose -f docker-compose.yml -f docker-compose.production.yml up -d --build`
5. Install nginx on your remote host
6. Setup reverse proxy to forward API requests to the deployed docker container listening at :9090
```
location /api/ {
   proxy_pass http://127.0.0.1:9090/api/;
}
```
7. Setup any of the available frontends ([React Native mobile app](https://github.com/SherApp/sher-mobile), [React web app](https://github.com/SherApp/sher-frontend))
