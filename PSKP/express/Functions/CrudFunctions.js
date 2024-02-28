const fs=require("fs")
module.exports={
    getAll: ()=>{
        let json=fs.readFileSync("telephone.json")
        return JSON.parse(json.toString())
    },
    add: (json)=>{
        let data=fs.readFileSync("telephone.json")
        var myObject = JSON.parse(data);

        let newData = {FIO: json.FIO, number: json.number};

        myObject.push(newData);

        var newData2 = JSON.stringify(myObject);
        fs.writeFileSync("telephone.json", newData2)
    },
    update: (json)=>{
        let result = JSON.parse(fs.readFileSync("telephone.json", "utf8"));
        delete result
        for (var i = 0; i < result.length; i++) {
            if (result[i]["number"] == json["hiddenNumber"]) {
                result[i]["FIO"] = json["FIO"];
                result[i]["number"] = json["number"]
            }
        }
        fs.writeFileSync("telephone.json", JSON.stringify(result));
    },
    delete: (json)=>{
        let data=fs.readFileSync("telephone.json")
        var myObject = JSON.parse(data);
        let FIO=json.FIO
        let newObject = myObject.filter(obj => obj.FIO !== FIO);
        newObject=JSON.stringify(newObject)
        fs.writeFileSync("telephone.json", newObject)
    }
}