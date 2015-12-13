<div class="page-header">
    <h1>Upload your pics</h1>
</div>


	<form name="photo" method="POST" action="/photographer/uploadPhotos" enctype="multipart/form-data">
	    Select image to upload:
	    <input type="file" name="fileToUpload" id="fileToUpload">

	    <select name="photosFor">
	    	<?php if (isset($photographer['selectedAddresses'])) { ?>
		    	<?php foreach ($photographer['selectedAddresses'] as $selectedAddress) { ?>
		    		<option value="<?php echo $selectedAddress->unique; ?>"><?php echo $selectedAddress->title; ?></option>
		    	<?php } ?>
		    <?php } ?>
	    </select>

	    <input type="submit" value="Upload Image" name="submit">
	</form>

	<hr/>

	<?php if (isset($photographer['trials'])) { ?>
		<?php foreach ($photographer['trials'] as $photos) { ?>
			<?php foreach ($photos as $photo) { ?>
				<img src="<?php echo $photo; ?>" class="img">
				<br/>
				<hr/>
			<?php } ?>
		<?php } ?>
	<?php } ?>


<hr/>

<a href="/photographer/logout" class="btn btn-default btn-lg btn-block" >logout</a>



