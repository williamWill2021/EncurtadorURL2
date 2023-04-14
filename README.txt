Nessa projeto temos dois endpoints, sendo eles GET/UrlEncurtadas e POST/UrlEncurtadas/EncurtarUrl.
Foi utilizado EntityFramework para conexão e manipulação dos dados em PostgreSql.


O endpoint POST/UrlEncurtadas/EncurtarUrl é responsável por gerar um alfanumérico aletório, associar a url informada e gravar essas informações no banco de dados.


O endpoint GET/UrlEncurtadas é responsável por obter a URL original e incrementar a contagem de acesso do link encurtado. É necessário passar como parâmetro o alfanumérico gerado pelo endpoint POST/UrlEncurtadas/EncurtarUrl.

