function bbMain() {
    var BB = new Binbox.API("//username.binbox.io/");
    var res = BB.encrypt(textWithCrLf);
    MyBB.createBB(res);
}
