FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch

RUN apt-get update && \
    apt-get install -y --no-install-recommends unzip && \
    rm -rf /var/lib/apt/lists/* && \
    wget -q -O /opt/sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=2134311 && unzip -qq /opt/sqlpackage.zip -d /opt/sqlpackage && chmod +x /opt/sqlpackage/sqlpackage && rm -f /opt/sqlpackage.zip

RUN apt-get update && \
    apt-get install -y curl gnupg apt-transport-https && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/microsoft.list' && \
    apt-get update && \
    apt-get install -y powershell