# Documentação

## API da Resistência

Essa API possui suporte a docker e um arquivo docker-compose que já sobe tanto a aplicação quanto o banco de dados sql server para testar

Os testes unitários estão utilizando um SQLite para que não haja dependência com um banco externo. 

Para rodar o projeto, basta ter o docker instalado e rodar os comandos a seguir na sequência informada

    docker-compose up -d db
    docker-compose build api
    docker-compose up api


O banco já irá subir com registro de 4 rebeldes. Adicionei uma consulta que traz todos os rebeldes da base.

A seguir segue as rotas da API

###   ​POST /api​/Negociacao
Entrada
```
{
  "codigoRebeldeUm": 1,
  "codigoRebeldeDois": 2,
  "itensDeTrocaRebeldeUm": {
    "ARMA": 1,
  },
  "itensDeTrocaRebeldeDois": {
    "AGUA": 2,
  }
}
```
 Na saída retorna `true` ou `false` caso a operação tenha sido bem sucedida ou mal sucedida
 

### POST /api/Rebeldes
Entrada

    {
      "nome": "string",
      "idade": 0,
      "genero": "string",
      "localizacao": {
        "latitude": "string",
        "longitude": "string",
        "nome": "string"
      },
      "inventario": {
        "AGUA": 0,
        "MUNICAO": 0,
        "COMIDA": 0
      }
    }

Saída

```
{
  "nome": "João Igor",
  "genero": "M",
  "idade": 22
}
```

### GET /api/Rebeldes

Saída
```
[
  {
    "id": 1,
    "nome": "Poe Dameron",
    "idade": 17,
    "genero": "M",
    "localizacao": null,
    "indicacaoTraidor": 3,
    "traidor": true,
    "inventario": {
      "AGUA": 10,
      "ARMA": 1,
      "COMIDA": 20,
      "MUNICAO": 50
    }
  },
]
```

### PUT /api/Rebeldes/{codigo}/localizacao
Entrada
Rota - código do rebelde

    {
      "latitude": "12345",
      "longitude": "78910",
      "nome": "Naboo"
    }

Saída

    {
      "latitude": "12345",
      "longitude": "78910",
      "nome": "Naboo"
    }


### POST api/Reportar/{codigo}

Entrada
Rota - código do rebelde

Saída
`true` ou `false`


### GET /api/Relatorios/percentual/rebeldes
Saída
```
{
  "percentual": 0.8
}
```

### GET /api/Relatorios/percentual/traidores
Saída
```
{
  "percentual": 0.8
}
```

### GET /api​/Relatorios​/pontos​/perdidos 
Saída
```
{
  "quantidade": 1
}
```

### GET /api/relatorios/itens/rebeldes
Saída
```
{
  "arma": 2,
  "municao": 40,
  "agua": 10,
  "comida": 20
}
```