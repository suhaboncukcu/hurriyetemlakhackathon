<div class="page-header">
    <h1>Signup and get started</h1>
</div>

<p>
	Just an email and a password should do the trick. 
</p>





<hr/>

<form class="form-horizontal" action="/photographer/createphotographer" method="POST">
  <div class="form-group">
    <label for="inputEmail3" class="col-sm-2 control-label">Email</label>
    <div class="col-sm-10">
      <input type="email" name="email" class="form-control" id="inputEmail3" placeholder="Email">
    </div>
  </div>
  <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Password</label>
    <div class="col-sm-10">
      <input type="password"  name="password"  class="form-control" id="inputPassword3" placeholder="Password">
    </div>
  </div>
  <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Identity Number</label>
    <div class="col-sm-10">
      <input type="text"  name="identity_no"  class="form-control" id="inputPassword3" placeholder="Identity Number">
    </div>
  </div>
  <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">IBAN</label>
    <div class="col-sm-10">
      <input type="text"  name="IBAN"  class="form-control" id="inputPassword3" placeholder="IBAN">
    </div>
  </div>
  <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Phone Number</label>
    <div class="col-sm-10">
      <input type="text"  name="phoneNumber"  class="form-control" id="inputPassword3" placeholder="+905555555">
    </div>
  </div>
  <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <button type="submit" class="btn btn-default">Sign Up</button>
    </div>
  </div>
</form>

<hr/>

<p>
	Also, you can check the photos from photographers if you already handled the sign up process.
</p>

<a href="/photographer/login" class="btn btn-default btn-lg btn-block" >See photos</a>



