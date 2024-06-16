let openapi = {
	"openapi": "3.0.0",
	"info": {
	  "title": "lab23",
	  "version": "1.0.0"
	},
	"servers": [
	  {
		"url": "http://localhost:3000"
	  }
	],
	"paths": {
	  "/TS": {
		"get": {
		  "tags": [
			"General"
		  ],
		  "summary": "Get Users",
		  "responses": {
			"200": {
			  "description": "Successful get user",
			  "content": {
				"application/json": {}
			  }
			}
		  }
		},
		"post": {
		  "tags": [
			"General"
		  ],
		  "summary": "Add User",
		  "requestBody": {
			"content": {
			  "application/json": {
				"schema": {
				  "type": "object",
				  "example": {
					"name": "Olga",
					"number": "1234567"
				  }
				}
			  }
			}
		  },
		  "responses": {
			"200": {
			  "description": "Successful add user",
			  "content": {
				"application/json": {}
			  }
			}
		  }
		},
		"delete": {
		  "tags": [
			"General"
		  ],
		  "summary": "Delete User",
			"requestBody": {
				"content": {
					"application/json": {
						"schema": {
							"type": "object",
							"example": {
								"name": "Olga"
							}
						}
					}
				}
			},

			"responses": {
			"200": {
			  "description": "Successful delete user",
				"application/json": {}
			}
		  }
		},
		"put": {
		  "tags": [
			"General"
		  ],
		  "summary": "Update User",
		  "requestBody": {
			"content": {
			  "application/json": {
				"schema": {
				  "type": "object",
				  "example": {
					"name": "Sidorov",
					"number": "1313"
				  }
				}
			  }
			}
		  },
		  "responses": {
			"200": {
			  "description": "update success",
			  "content": {
				"application/json": {}
			  }
			}
		  }
		}
	  }
	}
}
export default openapi;
