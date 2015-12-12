<div class="page-header">
    <h1>Signup and get started</h1>
</div>

<p>
	Just an email and a password should do the trick. 
</p>





<hr/>

<form class="form-horizontal" action="/provider/createProvider" method="POST">
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
    <div class="col-sm-offset-2 col-sm-10">
      <button type="submit" class="btn btn-default">Sign Up</button>
    </div>
  </div>
</form>

<hr/>

<p>
	Also, you can check the photos from photographers if you already handled the sign up process.
</p>

<a href="/provider/login" class="btn btn-default btn-lg btn-block" >See photos</a>



