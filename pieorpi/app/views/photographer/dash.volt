<div class="page-header">
    <h1>Upload your pics</h1>
</div>


	<form name="photo" method="POST" action="/photographer/uploadPhotos" enctype="multipart/form-data">
	    Select image to upload:
	    <input type="file" name="fileToUpload" id="fileToUpload">

	    <select name="photosFor">
	    	{% if photographer['selectedAddresses'] is defined %}
		    	{% for selectedAddress in photographer['selectedAddresses'] %}
		    		<option value="{{selectedAddress['unique']}}">{{selectedAddress['title']}}</option>
		    	{% endfor %}
		    {% endif %}
	    </select>

	    <input type="submit" value="Upload Image" name="submit">
	</form>

	<hr/>

	{% if photographer['trials'] is defined %}
		{% for photos in photographer['trials'] %}
			{% for photo in photos %}
				<img style="max-width: 300px; max-height: 200px;" src="/files/{{ photo }}" class="img">
				<br/>
				<hr/>
			{% endfor %}
		{% endfor %}
	{% endif %}


<hr/>

<a href="/photographer/logout" class="btn btn-default btn-lg btn-block" >logout</a>



