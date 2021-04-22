# DBServer Project
<p>Web API com o propósito de facilitar a escolha, de forma democrática, do local de almoço para um grupo de pessoas pré-definidas.</p>

# Como Usar

<h3>Dependências</h3>
<p>.NET Core 3.1</p>
<p>Suporte: https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/?view=aspnetcore-3.1</p>

<h3>Clonar repositório</h3>
<pre><code>git clone https://github.com/jlmjunior/dbserver-project</code></pre>

<h3>Requisições</h3>
<p><strong>[POST] Submit Vote</strong></p>
<p>Url: <code>api/Votation/submitvote</code></p>
<p>Request Body:</p>
<pre><code>{
    "IdUser":1,
    "IdRestaurant":2,
    "DateVote":"2021-04-20T11:16:40"
}</code></pre>

<p>Response Body: <code>200 Ok</code></p>
<pre><code>{
    "result": {
        "success": true,
        "message": "Voto realizado com sucesso"
    }
}</code></pre>

<p><strong>[GET] Get Votes</strong></p>
<p>Url: <code>api/Votation/getvotes</code></p>
<p>Response Body: <code>200 Ok</code></p>
<pre><code>{
    "result": [
        {
            "restaurantName": "King food",
            "votes": 2
        },
        {
            "restaurantName": "Bom apetite",
            "votes": 1
        }
    ]
}</code></pre>

<p><strong>[GET] Get Restaurants</strong></p>
<p>Url: <code>api/restaurant/getrestaurants</code></p>
<p>Response Body: <code>200 Ok</code></p>
<pre><code>{
    "result": [
        {
            "id": 1,
            "name": "Chefe do bairro",
            "imageLink": null,
            "isAvailable": true
        },
        {
            "id": 2,
            "name": "Bom apetite",
            "imageLink": null,
            "isAvailable": true
        },
        {
            "id": 3,
            "name": "Restaurante comida caseira",
            "imageLink": null,
            "isAvailable": true
        },
        {
            "id": 4,
            "name": "King food",
            "imageLink": null,
            "isAvailable": true
        }
    ]
}</code></pre>

# Regras do sistema

<p>* Um usuário só pode votar em um restaurante por dia;</p>
<p>* O mesmo restaurante não pode ser escolhido mais de uma vez durante a semana do voto;</p>
<p>* Horário limite de votação até às 11:45:00.</p>

# Possíveis melhorias
<p>* Adicionar autenticação de usuário;</p>
<p>* Cadastro de novos restaurantes.</p>
