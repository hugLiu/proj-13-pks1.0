db.createUser({
    user: "jurassic",
    pwd: "jurassic_pks_sz_87B515E8-0C4C-4B17-9A19-EE13D99DBAA7",
    roles: [ "root" ]
});

db.createUser({
    user: "PKSUser",
    pwd: "jurassic_pks_sz_498757d7-c90f-47d7-9139-7dc58c413db5",
    roles: [
        { role: "readWrite", db: "szpks" },
    ]
});
