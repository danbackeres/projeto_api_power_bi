# Projeto API Power BI

### Visão Geral

Bem-vindo ao **Projeto API Power BI**! Este projeto foi criado para demonstrar como incorporar relatórios do Power BI em uma aplicação ASP.NET Core usando a API do Power BI. A aplicação permite que você autentique, obtenha tokens de incorporação, e exiba relatórios diretamente na interface da sua aplicação web.

### Artigo LinkedIn

Para mais detalhes sobre como esse projeto foi concebido, bem como uma explicação passo a passo de como configurar a API do Power BI, confira o artigo completo no LinkedIn:

[Guia Completo: Como Incorporar Relatórios do Power BI Usando a API do Power BI](https://www.linkedin.com/in/daniel-hemerly-de-backer-256851321)

### Pré-requisitos

Antes de rodar o projeto, você precisará preencher algumas informações no arquivo **appsettings.json**. Certifique-se de que você tem as seguintes informações disponíveis:

~~~json
"PowerBI": {
  "ApplicationId": "<Seu Application ID>",
  "ApplicationSecret": "<Seu Application Secret>",
  "WorkspaceId": "<Seu Workspace ID>",
  "ReportId": "<Seu Report ID>",
  "TenantId": "<Seu Tenant ID>"
}
~~~~
* **ApplicationId**: O ID do aplicativo registrado no Azure AD.
* **ApplicationSecret**: O segredo gerado para o aplicativo no Azure AD.
* **WorkspaceId**: O ID do workspace onde o relatório está armazenado no Power BI.
* **ReportId**: O ID do relatório que você deseja incorporar.
* **TenantId**: O ID do tenant do Azure AD.

### Instruções para Rodar o Projeto com Docker

Para facilitar a execução do projeto, forneço uma configuração completa utilizando Docker. Siga as etapas abaixo para rodar a aplicação:

##### 1. Clone o Repositório

Primeiro, clone o repositório para a sua máquina local:

~~~bash
git clone https://github.com/danbackeres/projeto_api_power_bi.git
cd projeto_api_power_bi
~~~

##### 2. Configurar o appsettings.json

Abra o arquivo appsettings.json no diretório raiz e preencha os valores necessários na seção **PowerBI**.

##### 3. Executar com Docker
Para rodar a aplicação usando Docker, siga os passos abaixo:
###### Passo 1: Construir a Imagem Docker
Construa a imagem Docker executando o comando:
~~~bash
docker-compose up --build
~~~
Isso irá construir a imagem da aplicação e iniciar o contêiner.

###### Passo 2: Acessar a Aplicação
Após a construção bem-sucedida, você poderá acessar a aplicação no seu navegador:
~~~
http://localhost:8080
~~~

##### 4. Parar a Aplicação
Para parar a aplicação e remover os contêineres, utilize o seguinte comando:
~~~bash
docker-compose down
~~~ 
